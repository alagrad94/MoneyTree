using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoneyTree.Data;
using MoneyTree.Models;

namespace MoneyTree.Controllers {

    public class CostPerUnitController : Controller {

        private readonly ApplicationDbContext _context;

        public CostPerUnitController(ApplicationDbContext context) {

            _context = context;
        }

        // GET: CostPerUnits
        public async Task<IActionResult> Index() {

            MaintainCostPerUnitRecords(_context);

            var applicationDbContext = _context.CostPerUnit.Include(c => c.CostItem)
                .OrderBy(cpu => cpu.CostItemId)
                .ThenByDescending(cpu => cpu.StartDate);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CostPerUnits/Create
        public IActionResult Create() {

            MaintainCostPerUnitRecords(_context);

            ViewData["CostItemId"] = new SelectList(_context.CostItem, "Id", "ItemName");
            return View();
        }

        // POST: CostPerUnits/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StartDate,EndDate,Cost,CostItemId")] CostPerUnit costPerUnit) {

             DateTime Today = DateTime.UtcNow;
           
            if (costPerUnit.EndDate == null) {

                CostPerUnit CuurentCostPerUnit = _context.CostPerUnit.Where(cpu => cpu.CostItemId == costPerUnit.CostItemId)
                                                           .FirstOrDefault(cpu => cpu.EndDate == null);
                CuurentCostPerUnit.EndDate = Today.AddDays(-1);

                if (ModelState.IsValid) {

                    _context.Update(CuurentCostPerUnit);
                    await _context.SaveChangesAsync();

                    _context.Add(costPerUnit);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                ViewData["CostItemId"] = new SelectList(_context.CostItem, "Id", "ItemName", costPerUnit.CostItemId);
                return View(costPerUnit);

            } else {

                if (ModelState.IsValid) {

                    _context.Add(costPerUnit);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                ViewData["CostItemId"] = new SelectList(_context.CostItem, "Id", "ItemName", costPerUnit.CostItemId);
                return View(costPerUnit);
            }
        }

        // GET: CostPerUnits/Edit/5
        public async Task<IActionResult> Edit(int? id) {

            MaintainCostPerUnitRecords(_context);

            if (id == null) {

                return NotFound();
            }

            var costPerUnit = await _context.CostPerUnit.FindAsync(id);
            if (costPerUnit == null) {

                return NotFound();
            }

            ViewData["CostItemId"] = new SelectList(_context.CostItem, "Id", "ItemName", costPerUnit.CostItemId);
            return View(costPerUnit);
        }

        // POST: CostPerUnits/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StartDate,EndDate,Cost,CostItemId")] CostPerUnit costPerUnit) {

            if (id != costPerUnit.Id) {

                return NotFound();
            }

            if (ModelState.IsValid) {

                try {

                    _context.Update(costPerUnit);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {

                    if (!CostPerUnitExists(costPerUnit.Id)) {

                        return NotFound();

                    } else {

                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CostItemId"] = new SelectList(_context.CostItem, "Id", "ItemName", costPerUnit.CostItemId);
            return View(costPerUnit);
        }

        private bool CostPerUnitExists(int id) {
            return _context.CostPerUnit.Any(e => e.Id == id);
        }

        /// <summary>
        /// Called to ensure there is only 1 Cost Per Unit record per Cost Item with an EndDate of NULL.  This is necessary because there are places where the application determines the current cost of an item.  That determination is made by using the single record with an EndDate of NULL.
        /// </summary>
        public static void MaintainCostPerUnitRecords(ApplicationDbContext context) {
            
            List<CostItem> CostItems = context.CostItem.ToList();

            foreach (var item in CostItems) {

                List<CostPerUnit> CPURecords = context.CostPerUnit.Where(cpu => cpu.CostItemId == item.Id)
                    .OrderByDescending(cpu => cpu.StartDate).ToList();

                int NullEndDateCount = CPURecords.Count(cpu => cpu.EndDate == null);

                if ((NullEndDateCount != 1) || (NullEndDateCount == 1 && CPURecords[0].EndDate != null)) {

                    for (int i = 0; i < CPURecords.Count; i++) {

                        if (i == 0) {

                            CPURecords[i].EndDate = null;
                            context.Update(CPURecords[i]);
                            context.SaveChanges();

                        } else {

                            CPURecords[i].EndDate = CPURecords[i - 1].StartDate.AddDays(-1);
                            context.Update(CPURecords[i]);
                            context.SaveChanges();
                        }
                    }
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoneyTree.Data;
using MoneyTree.Models;

namespace MoneyTree.Controllers
{
    public class CostPerUnitController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CostPerUnitController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CostPerUnits
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CostPerUnit.Include(c => c.CostItem);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CostPerUnits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var costPerUnit = await _context.CostPerUnit
                .Include(c => c.CostItem)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (costPerUnit == null)
            {
                return NotFound();
            }

            return View(costPerUnit);
        }

        // GET: CostPerUnits/Create
        public IActionResult Create()
        {
            ViewData["CostItemId"] = new SelectList(_context.CostItem, "Id", "ItemName");
            return View();
        }

        // POST: CostPerUnits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StartDate,EndDate,Cost,CostItemId")] CostPerUnit costPerUnit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(costPerUnit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CostItemId"] = new SelectList(_context.CostItem, "Id", "ItemName", costPerUnit.CostItemId);
            return View(costPerUnit);
        }

        // GET: CostPerUnits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var costPerUnit = await _context.CostPerUnit.FindAsync(id);
            if (costPerUnit == null)
            {
                return NotFound();
            }
            ViewData["CostItemId"] = new SelectList(_context.CostItem, "Id", "ItemName", costPerUnit.CostItemId);
            return View(costPerUnit);
        }

        // POST: CostPerUnits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StartDate,EndDate,Cost,CostItemId")] CostPerUnit costPerUnit)
        {
            if (id != costPerUnit.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(costPerUnit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CostPerUnitExists(costPerUnit.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CostItemId"] = new SelectList(_context.CostItem, "Id", "ItemName", costPerUnit.CostItemId);
            return View(costPerUnit);
        }

        private bool CostPerUnitExists(int id)
        {
            return _context.CostPerUnit.Any(e => e.Id == id);
        }
    }
}

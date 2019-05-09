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

    public class ProjectCostController : Controller {

        private readonly ApplicationDbContext _context;

        public ProjectCostController(ApplicationDbContext context) {

            _context = context;
        }

        // GET: ProjectCosts/Create
        public IActionResult Create(int id) {

            CostPerUnitController.MaintainCostPerUnitRecords(_context);

            ViewData["CostItemId"] = new SelectList(_context.CostItem, "Id", "ItemName");
            ViewData["ProjectId"] = id;
            return View();
        }

        // POST: ProjectCosts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, ProjectCost projectCost) {

            projectCost.ProjectId = id;
            projectCost.Id = 0;

            CostPerUnit CuurentCostPerUnit = _context.CostPerUnit.Where(cpu => cpu.CostItemId == projectCost.CostItemId)
                                            .FirstOrDefault(cpu => cpu.EndDate == null);

            if (projectCost.DateUsed < CuurentCostPerUnit.StartDate) {

               CostPerUnit CostPerUnitCorrectDate = _context.CostPerUnit.Where(cpu => cpu.CostItemId == projectCost.CostItemId)
                                                .FirstOrDefault(cpu => projectCost.DateUsed <= cpu.EndDate
                                                                        && projectCost.DateUsed >= cpu.StartDate);
                projectCost.CostPerUnitId = CostPerUnitCorrectDate.Id;

            } else {

                projectCost.CostPerUnitId = CuurentCostPerUnit.Id;
            }

            ModelState.Remove("ProjectId");

            if (ModelState.IsValid) {

                _context.Add(projectCost);
                await _context.SaveChangesAsync();
                return RedirectToAction(actionName:"Details", controllerName: "Project", routeValues: new { id = projectCost.ProjectId });
            }

            ViewData["CostItemId"] = new SelectList(_context.CostItem, "Id", "ItemName", projectCost.CostItemId);
            ViewData["ProjectId"] = new SelectList(_context.Project, "Id", "ProjectName", projectCost.ProjectId);
            return View(projectCost);
        }

        // GET: ProjectCosts/Edit/5
        public async Task<IActionResult> Edit(int? id) {

            if (id == null) {

                return NotFound();
            }

            var projectCost = await _context.ProjectCost.FindAsync(id);

            List<CostPerUnit> CostPerUnitList = await _context.CostPerUnit.Where(cpu => cpu.CostItemId == projectCost.CostItemId).OrderByDescending(cpu => cpu.StartDate).ToListAsync();

            List<SelectListItem> CPUSelectList = new List<SelectListItem>();

            foreach (var item in CostPerUnitList) {
                
                string StartDateString = item.StartDate.ToShortDateString();
                string EndDateString = item.EndDate?.ToShortDateString() ?? "----";

                CPUSelectList.Add(new SelectListItem {
                    Value = item.Id.ToString(),
                    Text = $"Cost: ${item.Cost} - Date Range: {StartDateString} - {EndDateString}"
                });
            }

            ViewData["CostItemId"] = new SelectList(_context.CostItem, "Id", "ItemName", projectCost.CostItemId);
            ViewData["ProjectId"] = new SelectList(_context.Project, "Id", "ProjectName", projectCost.ProjectId);
            ViewData["CostPerUnitId"] = CPUSelectList;
            return View(projectCost);
        }

        // POST: ProjectCosts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, 
            [Bind("Id,CostItemId,ProjectId,CostPerUnitId,DateUsed,Quantity")] ProjectCost projectCost) {

            if (id != projectCost.Id) {

                return NotFound();
            }

            if (ModelState.IsValid) {

                try {

                    _context.Update(projectCost);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {

                    if (!ProjectCostExists(projectCost.Id)) {

                        return NotFound();
                    } else {

                        throw;
                    }
                }
                return RedirectToAction(actionName:"Details", controllerName: "Project", routeValues: new { id = projectCost.ProjectId });
            }

            List<CostPerUnit> CostPerUnitList = await _context.CostPerUnit.Where(cpu => cpu.CostItemId == projectCost.CostItemId).OrderByDescending(cpu => cpu.StartDate).ToListAsync();

            List<SelectListItem> CPUSelectList = new List<SelectListItem>();

            foreach (var item in CostPerUnitList) {
                
                string StartDateString = item.StartDate.ToShortDateString();
                string EndDateString = item.EndDate?.ToShortDateString() ?? "----";

                CPUSelectList.Add(new SelectListItem {
                    Value = item.Id.ToString(),
                    Text = $"Cost: ${item.Cost} - Date Range: {StartDateString} - {EndDateString}"
                });
            }

            ViewData["CostItemId"] = new SelectList(_context.CostItem, "Id", "ItemName", projectCost.CostItemId);
            ViewData["ProjectId"] = new SelectList(_context.Project, "Id", "ProjectName", projectCost.ProjectId);
            ViewData["CostPerUnitId"] = CPUSelectList;
            return View(projectCost);
        }

        // GET: ProjectCosts/Delete/5
        public async Task<IActionResult> Delete(int? id) {

            if (id == null) {

                return NotFound();
            }

            var projectCost = await _context.ProjectCost
                .Include(p => p.CostItem)
                .Include(p => p.Project)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (projectCost == null) {

                return NotFound();
            }

            return View(projectCost);
        }

        // POST: ProjectCosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {

            var projectCost = await _context.ProjectCost.FindAsync(id);
            _context.ProjectCost.Remove(projectCost);
            await _context.SaveChangesAsync();
            return RedirectToAction(actionName: "Details", controllerName: "Project", routeValues: new { id = projectCost.ProjectId });
        }

        private bool ProjectCostExists(int id) {

            return _context.ProjectCost.Any(e => e.Id == id);
        }
    }
}

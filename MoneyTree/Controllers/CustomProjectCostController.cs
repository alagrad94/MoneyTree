using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MoneyTree.Data;
using MoneyTree.Models;
using MoneyTree.Models.ViewModels;

namespace MoneyTree.Controllers {

    public class CustomProjectCostController : Controller {

        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;

        public CustomProjectCostController(ApplicationDbContext context, IConfiguration config) {
            _context = context;
            _config = config;
        }

        // GET: CustomProjectCosts/Create
        public IActionResult Create(int id) {

            DateTime today = DateTime.UtcNow;

            ProjectController controller = new ProjectController(_context, _config);

            CustomCostCreateViewModel viewModel = new CustomCostCreateViewModel {

                ProjectId = id,
                Project = controller.GetProjectById(id),
                CustomCosts = new List<CustomProjectCost>(),
            };

            CustomProjectCost customCost = new CustomProjectCost {
                ProjectId = id,
                ItemName = "",
                DateUsed = today,
                UnitOfMeasure = "",
                Category = "Custom",
                Quantity = 0
            };

            viewModel.CustomCosts.Add(customCost);

            ViewData["ProjectId"] = id;
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomCostCreateViewModel customProjectCosts) {


            List<CustomProjectCost> CustomProjectCostsInContext = await _context.CustomProjectCost
                .Where(cpc => cpc.ProjectId == customProjectCosts.ProjectId).ToListAsync();
            List<CustomProjectCost> CostsEntered = (customProjectCosts.CustomCosts?.Count > 0) ? 
                customProjectCosts.CustomCosts : customProjectCosts.RejectedEntries;
            List<CustomProjectCost> RejectecdEntries = new List<CustomProjectCost>();
            List<CustomProjectCost> UpdatedRecords = new List<CustomProjectCost>();

            Project Project = await _context.Project.FirstOrDefaultAsync(p => p.Id == customProjectCosts.ProjectId);
            DateTime CheckStartDate = Project.StartDate;

            foreach (var cost in CostsEntered.ToList()) {

                CustomProjectCost ExistingCost = new CustomProjectCost();

                if (cost.ItemName != null) {
                    ExistingCost = CustomProjectCostsInContext
                    .FirstOrDefault(cpc => cpc.ProjectId == cost.ProjectId
                    && cpc.ItemName.ToUpper() == cost.ItemName.ToUpper() && cpc.DateUsed == cost?.DateUsed);
                } else {
                    ExistingCost = null;
                }

                if (cost.ItemName == null || cost.UnitOfMeasure == null || cost.CostPerUnit <= 0 || 
                    cost.Quantity == 0 || cost.DateUsed < CheckStartDate ) {

                    CostsEntered.Remove(cost);
                    RejectecdEntries.Add(cost);
                }

                if (ExistingCost != null) {

                    ExistingCost.Quantity += cost.Quantity;
                    CostsEntered.Remove(cost);
                    UpdatedRecords.Add(ExistingCost);

                    _context.Update(ExistingCost);
                    await _context.SaveChangesAsync();
                }
            }

            foreach (var projectCost in CostsEntered) {

                _context.Add(projectCost);
                await _context.SaveChangesAsync();
            }

            if (RejectecdEntries.Count > 0 || UpdatedRecords.Count > 0) {


                CustomCostCreateViewModel viewModel = new CustomCostCreateViewModel {

                    ProjectId = customProjectCosts.ProjectId,
                    RejectedEntries = RejectecdEntries,
                    UpdatedRecords = UpdatedRecords
                };


                ViewData["ProjectId"] = customProjectCosts.ProjectId;
                return View("CreateFinish", viewModel);

            } else {

                return RedirectToAction("Details", "Project", new { id = customProjectCosts.ProjectId });
            }
        }

        // GET: CustomProjectCosts/Edit/5
        public async Task<IActionResult> Edit(int? id) {

            if (id == null) {
                return NotFound();
            }

            ProjectController controller = new ProjectController(_context, _config);

            var customProjectCost = await _context.CustomProjectCost.FindAsync(id);
            customProjectCost.Category = "Custom";
            customProjectCost.Project = controller.GetProjectById(customProjectCost.ProjectId);

            if (customProjectCost == null) {
                return NotFound();
            }

            ViewData["ProjectId"] = customProjectCost.ProjectId;
            return View(customProjectCost);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ItemName,ProjectId,Category, UnitOfMeasure,CostPerUnit,Quantity,DateUsed")] CustomProjectCost customProjectCost) {

            if (id != customProjectCost.Id) {
                return NotFound();
            }
            ModelState.Remove("Project");

            if (ModelState.IsValid) {
                try  {
                    _context.Update(customProjectCost);
                    await _context.SaveChangesAsync();
                }  catch (DbUpdateConcurrencyException)  {
                    if (!CustomProjectCostExists(customProjectCost.Id))  {
                        return NotFound();
                    }  else  {
                        throw;
                    }
                }

                return RedirectToAction("Details", "Project", new { id = customProjectCost.ProjectId });
            }

            ProjectController controller = new ProjectController(_context, _config);
            customProjectCost.Project = controller.GetProjectById(customProjectCost.ProjectId);

            ViewData["ProjectId"] = customProjectCost.ProjectId;
            return View(customProjectCost);
        }

        // GET: CustomProjectCosts/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null) {
                return NotFound();
            }

            var customProjectCost = await _context.CustomProjectCost
                .Include(c => c.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customProjectCost == null) {
                return NotFound();
            }

            return View(customProjectCost);
        }

        // POST: CustomProjectCosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {

            var customProjectCost = await _context.CustomProjectCost.FindAsync(id);
            _context.CustomProjectCost.Remove(customProjectCost);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Project", new { id = customProjectCost.ProjectId });
        }

        private bool CustomProjectCostExists(int id) {
            return _context.CustomProjectCost.Any(e => e.Id == id);
        }
    }
}

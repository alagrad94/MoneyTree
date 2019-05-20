using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MoneyTree.Data;
using MoneyTree.Models;

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
            CustomProjectCost model = new CustomProjectCost {
                ProjectId = id,
                Project = controller.GetProjectById(id),
                ItemName = "",
                DateUsed = today,
                UnitOfMeasure = "",
                Category = "Custom",
                Quantity = 0
            };

            ViewData["ProjectId"] = id;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, [Bind("Id,ItemName,ProjectId,Category, UnitOfMeasure,CostPerUnit,Quantity,DateUsed")] CustomProjectCost customProjectCost) {

            customProjectCost.Id = 0;
            
            ModelState.Remove("Project");

            if (ModelState.IsValid) {
                _context.Add(customProjectCost);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Project", new { id = customProjectCost.ProjectId });
            }

            ProjectController controller = new ProjectController(_context, _config);
            customProjectCost.Project = controller.GetProjectById(customProjectCost.ProjectId);

            ViewData["ProjectId"] = id;
            return View(customProjectCost);
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

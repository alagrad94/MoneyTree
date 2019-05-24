using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MoneyTree.Data;
using MoneyTree.Models;
using MoneyTree.Models.ViewModels;

namespace MoneyTree.Controllers {

    public class CustomEstimateCostController : Controller {

        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;

        public CustomEstimateCostController(ApplicationDbContext context, IConfiguration config) {
            _context = context;
            _config = config;
        }

        // GET: CustomEstimateCost/Create
        public IActionResult Create(int id) {

            DateTime today = DateTime.UtcNow;

            EstimateController controller = new EstimateController(_context, _config);

            CustomEstimateCostCreateViewModel viewModel = new CustomEstimateCostCreateViewModel {

                EstimateId = id,
                Estimate = controller.GetEstimateById(id),
                CustomCosts = new List<CustomEstimateCost>(),
            };

            CustomEstimateCost customCost = new CustomEstimateCost {
                EstimateId = id,
                ItemName = "",
                UnitOfMeasure = "",
                Category = "Custom",
                Quantity = 0
            };

            viewModel.CustomCosts.Add(customCost);

            ViewData["EstimateId"] = id;
            return View(viewModel);
        }

        // POST: CustomEstimateCost/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomEstimateCostCreateViewModel customEstimateCosts) {


            List<CustomEstimateCost> CustomEstimateCostsInContext = await _context.CustomEstimateCost
                .Where(cec => cec.EstimateId == customEstimateCosts.EstimateId).ToListAsync();
            List<CustomEstimateCost> CostsEntered = (customEstimateCosts.CustomCosts?.Count > 0) ?
                customEstimateCosts.CustomCosts : customEstimateCosts.RejectedEntries;
            List<CustomEstimateCost> RejectecdEntries = new List<CustomEstimateCost>();
            List<CustomEstimateCost> UpdatedRecords = new List<CustomEstimateCost>();

            Estimate Estimate = await _context.Estimate.FirstOrDefaultAsync(e => e.Id == customEstimateCosts.EstimateId);

            foreach (var cost in CostsEntered.ToList()) {

                CustomEstimateCost ExistingCost = new CustomEstimateCost();

                if (cost.ItemName != null) {
                    ExistingCost = CustomEstimateCostsInContext
                    .FirstOrDefault(cec => cec.EstimateId == cost.EstimateId && cec.ItemName.ToUpper() == cost.ItemName.ToUpper());
                } else {
                    ExistingCost = null;
                }

                if (cost.ItemName == null || cost.UnitOfMeasure == null || cost.CostPerUnit <= 0 ||
                    cost.Quantity == 0 || cost.MarkupPercent <= 0) {

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

            foreach (var estimateCost in CostsEntered) {

                _context.Add(estimateCost);
                await _context.SaveChangesAsync();
            }

            if (RejectecdEntries.Count > 0 || UpdatedRecords.Count > 0) {


                CustomEstimateCostCreateViewModel viewModel = new CustomEstimateCostCreateViewModel {

                    EstimateId = customEstimateCosts.EstimateId,
                    RejectedEntries = RejectecdEntries,
                    UpdatedRecords = UpdatedRecords
                };


                ViewData["EstimateId"] = customEstimateCosts.EstimateId;
                return View("CreateFinish", viewModel);

            }
            return RedirectToAction("Details", "Estimate", new { id = customEstimateCosts.EstimateId });
        }


        // GET: CustomEstimateCost/Edit/5
        public async Task<IActionResult> Edit(int? id) {

            if (id == null) {
                return NotFound();
            }

            EstimateController controller = new EstimateController(_context, _config);

            var customEstimateCost = await _context.CustomEstimateCost.FindAsync(id);
            customEstimateCost.Category = "Custom";
            customEstimateCost.Estimate = controller.GetEstimateById(customEstimateCost.EstimateId);

            if (customEstimateCost == null) {
                return NotFound();
            }

            ViewData["ProjectId"] = customEstimateCost.EstimateId;
            return View(customEstimateCost);
        }

        // POST: CustomEstimateCost/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, 
            [Bind("Id,ItemName,EstimateId,Category,UnitOfMeasure,CostPerUnit,MarkupPercent,Quantity")]
                 CustomEstimateCost customEstimateCost) {

            if (id != customEstimateCost.Id) {
                return NotFound();
            }

            ModelState.Remove("Estimate");

            if (ModelState.IsValid) {
               
                _context.Update(customEstimateCost);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Estimate", new { id = customEstimateCost.EstimateId });
            }

            EstimateController controller = new EstimateController(_context, _config);
            customEstimateCost.Estimate = controller.GetEstimateById(customEstimateCost.EstimateId);

            ViewData["ProjectId"] = customEstimateCost.EstimateId;
            return View(customEstimateCost);
        }

        // GET: CustomEstimateCost/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null) {
                return NotFound();
            }

            var customEstimateCost = await _context.CustomEstimateCost
                .Include(c => c.Estimate)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customEstimateCost == null) {
                return NotFound();
            }

            return View(customEstimateCost);
        }

        // POST: CustomEstimateCost/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {

            var customEstimateCost = await _context.CustomEstimateCost.FindAsync(id);
            _context.CustomEstimateCost.Remove(customEstimateCost);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Estimate", new { id = customEstimateCost.EstimateId });
        }

    }
}

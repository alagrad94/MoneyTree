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

    public class EstimateCostController : Controller {

        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;

        public EstimateCostController(ApplicationDbContext context, IConfiguration config) {
            _context = context;
            _config = config;
        }

        // GET: EstimateCosts/Create
        public async Task<IActionResult> Create(int id) {

            DateTime Today = DateTime.UtcNow;
            EstimateController controller = new EstimateController(_context, _config);
            EstimateCostCreateViewModel model = new EstimateCostCreateViewModel {

                EstimateId = id,
                Estimate = controller.GetEstimateById(id),
                Costs = new List<EstimateCost>(),
                CostItems = new List<CostItem>()
            };

            model.CostItems = await _context.CostItem
                .Include(ci => ci.CostCategory)
                .Include(ci => ci.UnitOfMeasure)
                .OrderBy(ci => ci.ItemName)
                .ToListAsync();

            EstimateCost Cost = new EstimateCost {
                EstimateId = id
            };

            model.Costs.Add(Cost);

            return View(model);
        }

        // POST: EstimateCosts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EstimateCostCreateViewModel estimateCosts) {

            List<EstimateCost> EstimateCostsInContext = await _context.EstimateCost
                .Where(ec => ec.EstimateId == estimateCosts.EstimateId).ToListAsync();
            List<EstimateCost> CostsEntered = (estimateCosts.Costs?.Count > 0) ? estimateCosts.Costs : estimateCosts.RejectedEntries;
            List<EstimateCost> RejectecdEntries = new List<EstimateCost>();
            List<EstimateCost> UpdatedRecords = new List<EstimateCost>();

            foreach (var cost in CostsEntered.ToList()) {

                cost.EstimateId = estimateCosts.EstimateId;

                EstimateCost ExistingCost = EstimateCostsInContext
                    .FirstOrDefault(ec => ec.EstimateId == cost.EstimateId && ec.CostItemId == cost.CostItemId);

                if (cost.Quantity <= 0 || cost.CostItemId == 0) {

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

                estimateCost.EstimateId = estimateCosts.EstimateId;
                _context.Add(estimateCost);
                await _context.SaveChangesAsync();
            }

            if (RejectecdEntries.Count > 0 || UpdatedRecords.Count > 0) {

                if (RejectecdEntries.Count > 0) {
                    foreach (var item in RejectecdEntries) {
                        item.CostItem = _context.CostItem.FirstOrDefault(ci => ci.Id == item.CostItemId);
                        item.CostPerUnit = _context.CostPerUnit.FirstOrDefault(cpu => cpu.CostItemId == item.CostItemId && cpu.EndDate == null).Cost;
                    }
                }

                if (UpdatedRecords.Count > 0) {
                    foreach (var item in UpdatedRecords) {
                        item.CostItem = _context.CostItem.FirstOrDefault(ci => ci.Id == item.CostItemId);
                        item.CostPerUnit = _context.CostPerUnit.FirstOrDefault(cpu => cpu.CostItemId == item.CostItemId && cpu.EndDate == null).Cost;
                    }
                }

                EstimateCostCreateViewModel viewModel = new EstimateCostCreateViewModel {

                    EstimateId = estimateCosts.EstimateId,
                    CostItems = new List<CostItem>(),
                    RejectedEntries = RejectecdEntries,
                    UpdatedRecords = UpdatedRecords
                };

                viewModel.CostItems = _context.CostItem.Include(ci => ci.UnitOfMeasure).OrderBy(ci => ci.ItemName).ToList();

                return View("CreateFinish", viewModel);

            } else {

                return RedirectToAction("Details", "Estimate", new { id = estimateCosts.EstimateId });
            }
        }


        // GET: EstimateCosts/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return NotFound();
            }

            var estimateCost = await _context.EstimateCost.FindAsync(id);
            estimateCost.Estimate = await _context.Estimate.FindAsync(estimateCost.EstimateId);
            estimateCost.CostItem = await _context.CostItem.FindAsync(estimateCost.CostItemId);

            if (estimateCost == null) {
                return NotFound();
            }


            ViewData["CostItemId"] = estimateCost.CostItem;
            ViewData["EstimateId"] = estimateCost.EstimateId;

            return View(estimateCost);
        }

        // POST: EstimateCosts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EstimateId,CostItemId,Quantity")] EstimateCost estimateCost) {

            if (id != estimateCost.Id) {
                return NotFound();
            }

            if (ModelState.IsValid) {

                _context.Update(estimateCost);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Estimate", new { id = estimateCost.EstimateId });
            }

            estimateCost.Estimate = await _context.Estimate.FindAsync(estimateCost.EstimateId);
            estimateCost.CostItem = await _context.CostItem.FindAsync(estimateCost.CostItemId);

            ViewData["CostItemId"] = estimateCost.CostItem;
            ViewData["EstimateId"] = estimateCost.EstimateId;

            return View(estimateCost);
        }

        // GET: EstimateCosts/Delete/5
        public async Task<IActionResult> Delete(int? id)  {

            if (id == null)  {
                return NotFound();
            }

            var estimateCost = await _context.EstimateCost
                .Include(e => e.CostItem)
                .Include(e => e.Estimate)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (estimateCost == null) {
                return NotFound();
            }

            return View(estimateCost);
        }

        // POST: EstimateCosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)  {
            var estimateCost = await _context.EstimateCost.FindAsync(id);
            _context.EstimateCost.Remove(estimateCost);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Estimate", new { id = estimateCost.EstimateId });
        }
    }
}

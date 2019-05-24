using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoneyTree.Data;
using MoneyTree.Models;
using MoneyTree.Models.ViewModels;

namespace MoneyTree.Controllers {

    public class CostCategoryController : Controller {

        private readonly ApplicationDbContext _context;

        public CostCategoryController(ApplicationDbContext context) {

            _context = context;
        }

        // GET: CostCategories
        public async Task<IActionResult> Index() {

            return View(await _context.CostCategory.OrderBy(cc => cc.CategoryName).ToListAsync());
        }

        // GET: CostCategories/Create
        public IActionResult Create() {

            return View();
        }

        // POST: CostCategories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CategoryName,MarkupPercent")] CostCategory costCategory) {

            if (ModelState.IsValid) {

                CostCategory ExistingCategory = _context.CostCategory
                    .FirstOrDefault(cc => cc.CategoryName.ToUpper() == costCategory.CategoryName.ToUpper());

                if (ExistingCategory == null) {

                    _context.Add(costCategory);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                return View("CreateDuplicate", costCategory);
            }
            return View(costCategory);
        }

        // GET: CostCategories/Edit/5
        public async Task<IActionResult> Edit(int? id) {

            if (id == null) {

                return NotFound();
            }

            var costCategory = await _context.CostCategory.FindAsync(id);

            if (costCategory == null) {
                return NotFound();
            }
            return View(costCategory);
        }

        // POST: CostCategories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CategoryName,MarkupPercent")] CostCategory costCategory) {

            if (id != costCategory.Id) {

                return NotFound();
            }

            if (ModelState.IsValid) {

                CostCategory ExistingCategory = _context.CostCategory
                    .FirstOrDefault(cc => cc.CategoryName.ToUpper() == costCategory.CategoryName.ToUpper());

                if (ExistingCategory == null) {

                    _context.Update(costCategory);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                return View("EditDuplicate", costCategory);
            }
            return View(costCategory);
        }

        // GET: CostCategories/Delete/5
        public ActionResult Delete(int? id) {

            if (id == null) {

                return NotFound();
            }

            int catId = id.GetValueOrDefault();

            DeleteCategoryViewModel viewModel = new DeleteCategoryViewModel {

                OldCategoryId = catId,
                CostCategories = _context.CostCategory.ToList(),
                CostCategory = _context.CostCategory.FirstOrDefault(c => c.Id == id)
            };

            return View(viewModel);
        }

        // POST: CostCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(DeleteCategoryViewModel viewModel) {

            int NewCategoryId = viewModel.NewCategoryId;

            ReassignItemsToNewCategory(viewModel.OldCategoryId, NewCategoryId);
            await _context.SaveChangesAsync();
            var costCategory = await _context.CostCategory.FindAsync(viewModel.OldCategoryId);
            _context.CostCategory.Remove(costCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private void ReassignItemsToNewCategory(int oldCatId, int newCatId) {

            List<CostItem> Items = _context.CostItem.Where(i => i.CostCategoryId == oldCatId).ToList();

            foreach (var item in Items) {

                item.CostCategoryId = newCatId;
                _context.Update(item);
            }
        }
    }
}

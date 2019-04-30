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
    public class CostCategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CostCategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CostCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.CostCategory.ToListAsync());
        }

        // GET: CostCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CostCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CategoryName")] CostCategory costCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(costCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(costCategory);
        }

        // GET: CostCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var costCategory = await _context.CostCategory.FindAsync(id);
            if (costCategory == null)
            {
                return NotFound();
            }
            return View(costCategory);
        }

        // POST: CostCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CategoryName")] CostCategory costCategory)
        {
            if (id != costCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(costCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CostCategoryExists(costCategory.Id))
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
            return View(costCategory);
        }

        // GET: CostCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var costCategory = await _context.CostCategory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (costCategory == null)
            {
                return NotFound();
            }

            return View(costCategory);
        }

        // POST: CostCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var costCategory = await _context.CostCategory.FindAsync(id);
            _context.CostCategory.Remove(costCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CostCategoryExists(int id)
        {
            return _context.CostCategory.Any(e => e.Id == id);
        }
    }
}

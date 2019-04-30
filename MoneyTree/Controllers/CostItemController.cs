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
    public class CostItemController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CostItemController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CostItems
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CostItem.Include(c => c.CostCategory).Include(c => c.UnitOfMeasure);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CostItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var costItem = await _context.CostItem
                .Include(c => c.CostCategory)
                .Include(c => c.UnitOfMeasure)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (costItem == null)
            {
                return NotFound();
            }

            return View(costItem);
        }

        // GET: CostItems/Create
        public IActionResult Create()
        {
            ViewData["CostCategoryId"] = new SelectList(_context.CostCategory, "Id", "CategoryName");
            ViewData["UnitOfMeasureId"] = new SelectList(_context.UnitOfMeasure, "Id", "UnitName");
            return View();
        }

        // POST: CostItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UnitOfMeasureId,CostCategoryId")] CostItem costItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(costItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CostCategoryId"] = new SelectList(_context.CostCategory, "Id", "CategoryName", costItem.CostCategoryId);
            ViewData["UnitOfMeasureId"] = new SelectList(_context.UnitOfMeasure, "Id", "UnitName", costItem.UnitOfMeasureId);
            return View(costItem);
        }

        // GET: CostItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var costItem = await _context.CostItem.FindAsync(id);
            if (costItem == null)
            {
                return NotFound();
            }
            ViewData["CostCategoryId"] = new SelectList(_context.CostCategory, "Id", "CategoryName", costItem.CostCategoryId);
            ViewData["UnitOfMeasureId"] = new SelectList(_context.UnitOfMeasure, "Id", "UnitName", costItem.UnitOfMeasureId);
            return View(costItem);
        }

        // POST: CostItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UnitOfMeasureId,CostCategoryId")] CostItem costItem)
        {
            if (id != costItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(costItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CostItemExists(costItem.Id))
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
            ViewData["CostCategoryId"] = new SelectList(_context.CostCategory, "Id", "CategoryName", costItem.CostCategoryId);
            ViewData["UnitOfMeasureId"] = new SelectList(_context.UnitOfMeasure, "Id", "UnitName", costItem.UnitOfMeasureId);
            return View(costItem);
        }

        private bool CostItemExists(int id)
        {
            return _context.CostItem.Any(e => e.Id == id);
        }
    }
}

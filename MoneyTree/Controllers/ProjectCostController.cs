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
    public class ProjectCostController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjectCostController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProjectCosts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ProjectCost.Include(p => p.CostItem).Include(p => p.Project);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ProjectCosts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectCost = await _context.ProjectCost
                .Include(p => p.CostItem)
                .Include(p => p.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (projectCost == null)
            {
                return NotFound();
            }

            return View(projectCost);
        }

        // GET: ProjectCosts/Create
        public IActionResult Create()
        {
            ViewData["CostItemId"] = new SelectList(_context.CostItem, "Id", "ItemName");
            ViewData["ProjectId"] = new SelectList(_context.Project, "Id", "ProjectName");
            return View();
        }

        // POST: ProjectCosts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CostItemId,ProjectId,DateUsed,Quantity")] ProjectCost projectCost)
        {
            if (ModelState.IsValid)
            {
                _context.Add(projectCost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CostItemId"] = new SelectList(_context.CostItem, "Id", "ItemName", projectCost.CostItemId);
            ViewData["ProjectId"] = new SelectList(_context.Project, "Id", "ProjectName", projectCost.ProjectId);
            return View(projectCost);
        }

        // GET: ProjectCosts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectCost = await _context.ProjectCost.FindAsync(id);
            if (projectCost == null)
            {
                return NotFound();
            }
            ViewData["CostItemId"] = new SelectList(_context.CostItem, "Id", "ItemName", projectCost.CostItemId);
            ViewData["ProjectId"] = new SelectList(_context.Project, "Id", "ProjectName", projectCost.ProjectId);
            return View(projectCost);
        }

        // POST: ProjectCosts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CostItemId,ProjectId,DateUsed,Quantity")] ProjectCost projectCost)
        {
            if (id != projectCost.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(projectCost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectCostExists(projectCost.Id))
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
            ViewData["CostItemId"] = new SelectList(_context.CostItem, "Id", "ItemName", projectCost.CostItemId);
            ViewData["ProjectId"] = new SelectList(_context.Project, "Id", "ProjectName", projectCost.ProjectId);
            return View(projectCost);
        }

        // GET: ProjectCosts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectCost = await _context.ProjectCost
                .Include(p => p.CostItem)
                .Include(p => p.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (projectCost == null)
            {
                return NotFound();
            }

            return View(projectCost);
        }

        // POST: ProjectCosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var projectCost = await _context.ProjectCost.FindAsync(id);
            _context.ProjectCost.Remove(projectCost);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectCostExists(int id)
        {
            return _context.ProjectCost.Any(e => e.Id == id);
        }
    }
}

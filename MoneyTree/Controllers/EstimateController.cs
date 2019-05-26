using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MoneyTree.Data;
using MoneyTree.Models;

namespace MoneyTree.Controllers {

    public class EstimateController : Controller {

        private readonly IConfiguration _config;
        private readonly ApplicationDbContext _context;

        public SqlConnection Connection {
            get {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        public EstimateController(ApplicationDbContext context, IConfiguration config) {
            _context = context;
            _config = config;
        }

        // GET: Estimates
        public async Task<IActionResult> Index() {

            List<Estimate> estimatesGet = new List<Estimate>();

            foreach (var estimate in await _context.Estimate.ToListAsync()) {

                estimatesGet.Add(GetEstimateById(estimate.Id));
            }

            IEnumerable<Estimate> estimates = estimatesGet.OrderByDescending(e => e.EstimateDate);
            return View(estimates);
        }

        // GET: Estimates/Details/5
        public IActionResult Details(int? id) {
            if (id == null) {
                return NotFound();
            }

            var estimate = GetEstimateById(id);

            estimate.Categories.Sort((a,b) => string.Compare(a.CategoryName, b.CategoryName, StringComparison.CurrentCulture));

            if (estimate == null) {
                return NotFound();
            }

            Dictionary<string, double> categoryTotals = new Dictionary<string, double>();

            foreach (var category in estimate.Categories) {

                double categoryTotal = 0.00;

                foreach (var cost in estimate.EstimateCosts) {

                    if (cost.CostItem.CostCategory.CategoryName == category.CategoryName) {

                        categoryTotal += cost.Total;
                    }
                }
                categoryTotals.Add(category.CategoryName, categoryTotal);
            }

            double customTotal = 0.00;
            foreach (var cost in estimate.CustomCosts) {
                customTotal += cost.TotalCost;
            }

            categoryTotals.Add("Custom", customTotal);

            ViewData["CategoryTotals"] = categoryTotals;
            return View(estimate);
        }

        // GET: Estimates/PrintDetails/5
        public IActionResult PrintDetails(int? id) {
            if (id == null) {
                return NotFound();
            }

            var estimate = GetEstimateById(id);

            estimate.Categories.Sort((a, b) => string.Compare(a.CategoryName, b.CategoryName, StringComparison.CurrentCulture));

            if (estimate == null) {
                return NotFound();
            }

            Dictionary<string, double> categoryTotals = new Dictionary<string, double>();

            foreach (var category in estimate.Categories) {

                double categoryTotal = 0.00;

                foreach (var cost in estimate.EstimateCosts) {

                    if (cost.CostItem.CostCategory.CategoryName == category.CategoryName) {

                        categoryTotal += cost.Total;
                    }
                }
                categoryTotals.Add(category.CategoryName, categoryTotal);
            }

            double customTotal = 0.00;
            foreach (var cost in estimate.CustomCosts) {
                customTotal += cost.TotalCost;
            }

            categoryTotals.Add("Custom", customTotal);

            ViewData["CategoryTotals"] = categoryTotals;
            return View(estimate);
        }

        // GET: Estimates/Create
        public IActionResult Create()  {

            var today = DateTime.Now;

            Estimate estimate = new Estimate {

                EstimateDate = today,
                ExpirationDate = today
            };

            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "FullName");
            return View(estimate);
        }

        // POST: Estimates/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EstimateDate,ExpirationDate")] Estimate estimate) {
            if (ModelState.IsValid) {
                _context.Add(estimate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(estimate);
        }

        // GET: Estimates/Edit/5
        public async Task<IActionResult> Edit(int? id)  {
            if (id == null) {
                return NotFound();
            }

            var estimate = await _context.Estimate.FindAsync(id);

            if (estimate == null) {
                return NotFound();
            }

            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "FullName");
            return View(estimate);
        }

        // POST: Estimates/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EstimateDate,ExpirationDate")] Estimate estimate) {
            if (id != estimate.Id)  {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(estimate);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!EstimateExists(estimate.Id)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(estimate);
        }

        // GET: Estimates/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null) {
                return NotFound();
            }

            var estimate = await _context.Estimate
                .Include(e => e.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estimate == null) {
                return NotFound();
            }

            return View(estimate);
        }

        // POST: Estimates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var estimate = await _context.Estimate.FindAsync(id);
            _context.Estimate.Remove(estimate);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstimateExists(int id) {
            return _context.Estimate.Any(e => e.Id == id);
        }

        public Estimate GetEstimateById(int? id) {

            using (SqlConnection conn = Connection) {

                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand()) {

                    cmd.CommandText = @"SELECT e.id AS EstimateId, e.ProjectTitle, e.EstimateDate, 
                                               e.ExpirationDate, c.Id AS CustomerId, c.FirstName, c.LastName, 
                                               c.PhoneNumber, c.Email, ec.Id AS EstimateCostId, ci.Id AS CostItemId, 
                                               ci.ItemName, um.Id AS UnitId, um.UnitName, cc.Id AS CostCategoryId, 
                                               cc.CategoryName, cc.MarkupPercent, cpu.Cost, ec.Quantity, cec.Id AS CustomCostID, 
                                               cec.ItemName AS CustomItem, cec.CostPerUnit AS CustomCPU, cec.Quantity AS CustomQuantity, 
                                               cec.MarkupPercent, cec.UnitOfMeasure AS CustomUnits, cec.Category AS CustomCategory
                                          FROM Estimate e
                                     LEFT JOIN Customer c ON e.CustomerId = c.Id
                                     LEFT JOIN EstimateCost ec on ec.EstimateId = e.id
                                     LEFT JOIN CustomEstimateCost cec on cec.EstimateId = e.id
                                     LEFT JOIN CostItem ci ON ec.CostItemId = ci.Id
                                     LEFT JOIN CostPerUnit cpu ON cpu.CostItemId = ec.CostItemId AND cpu.EndDate IS NULL
                                     LEFT JOIN CostCategory cc ON ci.CostCategoryId = cc.Id
                                     LEFT JOIN UnitOfMeasure um ON ci.UnitOfMeasureId = um.Id 
                                         WHERE e.Id = @id
                                      ORDER BY e.EstimateDate DESC;";

                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    SqlDataReader reader = cmd.ExecuteReader();

                    Estimate estimate = null;

                    while (reader.Read()) {

                        if (estimate == null) {

                            estimate = new Estimate {

                                Id = reader.GetInt32(reader.GetOrdinal("EstimateId")),
                                ProjectTitle = reader.GetString(reader.GetOrdinal("ProjectTitle")),
                                EstimateDate = reader.GetDateTime(reader.GetOrdinal("EstimateDate")),
                                ExpirationDate = reader.GetDateTime(reader.GetOrdinal("ExpirationDate")),
                                Customer = new Customer {
                                    Id = reader.GetInt32(reader.GetOrdinal("CustomerId")),
                                    FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                    LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                    Email = reader.GetString(reader.GetOrdinal("Email")),
                                    PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber"))

                                },
                                EstimateCosts = new List<EstimateCost>(),
                                Categories = new List<CostCategory>(),
                                CustomCosts = new List<CustomEstimateCost>()
                            };
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("CustomCostId"))) {

                            int customCostId = reader.GetInt32(reader.GetOrdinal("CustomCostId"));

                            if (!estimate.CustomCosts.Any(cpc => cpc.Id == customCostId)) {

                                CustomEstimateCost customCost = new CustomEstimateCost {
                                    Id = reader.GetInt32(reader.GetOrdinal("CustomCostId")),
                                    ItemName = reader.GetString(reader.GetOrdinal("CustomItem")),
                                    CostPerUnit = reader.GetDouble(reader.GetOrdinal("CustomCPU")),
                                    Quantity = reader.GetInt32(reader.GetOrdinal("CustomQuantity")),
                                    UnitOfMeasure = reader.GetString(reader.GetOrdinal("CustomUnits")),
                                    Category = reader.GetString(reader.GetOrdinal("CustomCategory")),
                                    MarkupPercent = reader.GetDouble(reader.GetOrdinal("MarkupPercent"))
                                };
                                estimate.CustomCosts.Add(customCost);
                            }
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("EstimateCostId"))) {

                            int estimateCostId = reader.GetInt32(reader.GetOrdinal("EstimateCostId"));

                            if (!estimate.EstimateCosts.Any(ec => ec.Id == estimateCostId)) {

                                EstimateCost estimateCost = new EstimateCost {

                                    Id = reader.GetInt32(reader.GetOrdinal("EstimateCostId")),
                                    Quantity = reader.GetDouble(reader.GetOrdinal("Quantity")),
                                    CostItem = new CostItem {
                                        Id = reader.GetInt32(reader.GetOrdinal("CostItemId")),
                                        ItemName = reader.GetString(reader.GetOrdinal("ItemName")),
                                        UnitOfMeasure = new UnitOfMeasure {
                                            Id = reader.GetInt32(reader.GetOrdinal("UnitId")),
                                            UnitName = reader.GetString(reader.GetOrdinal("UnitName"))
                                        },
                                        CostCategory = new CostCategory{
                                            Id = reader.GetInt32(reader.GetOrdinal("CostCategoryId")),
                                            CategoryName = reader.GetString(reader.GetOrdinal("CategoryName")),
                                            MarkupPercent = reader.GetDouble(reader.GetOrdinal("MarkupPercent"))
                                        },
                                    },
                                    
                                    CostPerUnit =  reader.GetDouble(reader.GetOrdinal("Cost"))
                                };
                                estimate.EstimateCosts.Add(estimateCost);
                            }
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("CostCategoryId"))) {

                            int costCategoryId = reader.GetInt32(reader.GetOrdinal("CostCategoryId"));

                            if (!estimate.Categories.Any(c => c.Id == costCategoryId)) {

                                CostCategory category = new CostCategory {

                                    Id = reader.GetInt32(reader.GetOrdinal("CostCategoryId")),
                                    CategoryName = reader.GetString(reader.GetOrdinal("CategoryName")),
                                    MarkupPercent = reader.GetDouble(reader.GetOrdinal("MarkupPercent"))
                                };
                                estimate.Categories.Add(category);
                            }
                        }
                    }
                    reader.Close();
                    return estimate;
                }
            }
        }
    }
}

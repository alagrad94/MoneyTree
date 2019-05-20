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

    public class ProjectController : Controller {

        private readonly IConfiguration _config;
        private readonly ApplicationDbContext _context;

        public SqlConnection Connection {
            get {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        public ProjectController(ApplicationDbContext context, IConfiguration config) {

            _context = context;
            _config = config;
        }

        // GET: Projects
        public async Task<IActionResult> Index() { 

            List<Project> projectsGet = new List<Project>();

            foreach (var project in await _context.Project.ToListAsync()) {

              projectsGet.Add(GetProjectById(project.Id));
            }

            IEnumerable<Project> projects = projectsGet.OrderByDescending(p => p.StartDate.Date);
            return View(projects);
        }

        // GET: Projects/Details/5
        public IActionResult Details(int? id) {

            if (id == null) {

                return NotFound();
            }

            var project = GetProjectById(id);

            if (project == null) {

                return NotFound();
            }

            return View(project);
        }

        // GET: Projects/Create
        public async Task<IActionResult> Create() {

            List<SelectListItem> CustomerOptions = await _context.Customer.OrderBy(c => c.LastName).Select(c => new SelectListItem {
                Value = c.Id.ToString(),
                Text = c.FullName
            }).ToListAsync();

            CustomerOptions?.Insert(0, new SelectListItem { Value = "0", Text = "Select A Customer", Selected = true });

            var today = DateTime.Now.ToString("yyyy-MM-dd");

            ViewData["DefaultDate"] = today;
            ViewData["CustomerId"] = CustomerOptions;
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,ProjectName,StartDate,CompletionDate,AmountCharged,CustomerId,UserId")] Project project) {

            if (ModelState.IsValid) {

                _context.Add(project);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            List<SelectListItem> CustomerOptions = await _context.Customer.OrderBy(c => c.LastName).Select(c => new SelectListItem {
                Value = c.Id.ToString(),
                Text = c.FullName
            }).ToListAsync();

            CustomerOptions?.Insert(0, new SelectListItem { Value = "0", Text = "Select A Customer", Selected = true });

            ViewData["CustomerId"] = CustomerOptions;
            return View(project);
        }

        // GET: Projects/Edit/5
        public async Task<IActionResult> Edit(int? id) {

            if (id == null) {

                return NotFound();
            }

            var project = await _context.Project.FindAsync(id);
            if (project == null) {

                return NotFound();
            }
            List<SelectListItem> CustomerOptions = await _context.Customer.OrderBy(c => c.LastName).Select(c => new SelectListItem {
                Value = c.Id.ToString(),
                Text = c.FullName
            }).ToListAsync();

            CustomerOptions?.Insert(0, new SelectListItem { Value = "0", Text = "Select A Customer", Selected = true });

            ViewData["CustomerId"] = CustomerOptions;
            return View(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, 
            [Bind("Id,ProjectName,StartDate,CompletionDate,AmountCharged,CustomerId,UserId,IsComplete")] Project project) {

            if (id != project.Id) {

                return NotFound();
            }

            if (ModelState.IsValid) {

                try {

                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) {

                    if (!ProjectExists(project.Id)) {

                        return NotFound();
                    }

                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            List<SelectListItem> CustomerOptions = await _context.Customer.OrderBy(c => c.LastName).Select(c => new SelectListItem {
                Value = c.Id.ToString(),
                Text = c.FullName
            }).ToListAsync();

            CustomerOptions?.Insert(0, new SelectListItem { Value = "0", Text = "Select A Customer", Selected = true });

            ViewData["CustomerId"] = CustomerOptions;
            return View(project);
        }

        // GET: Projects/Delete/5
        public async Task<IActionResult> Delete(int? id) {

            if (id == null) {

                return NotFound();
            }

            var project = await _context.Project
                .Include(p => p.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null) {

                return NotFound();
            }

            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {

            var project = await _context.Project.FindAsync(id);
            _context.Project.Remove(project);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(int id) {

            return _context.Project.Any(e => e.Id == id);
        }

        public Project GetProjectById(int? id) {

            using (SqlConnection conn = Connection) {

                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand()) {

                    cmd.CommandText = @"SELECT p.Id AS ProjectId, p.ProjectName, p.StartDate AS ProjectStart, p.IsComplete,
                                               p.CompletionDate AS ProjectEnd, p.AmountCharged, c.Id AS CustomerId, 
                                               c.FirstName, c.LastName, c.PhoneNumber, c.Email, pc.Id AS ProjectCostId, 
                                               pc.DateUsed, pc.Quantity, ci.Id AS CostItemId, ci.ItemName, um.Id AS UnitId,
                                               um.UnitName, cc.Id AS CostCategoryId, cc.CategoryName, cpu.Id AS CostPerUnitId,
                                               cpu.Cost, cpu.StartDate AS CostStart, cpu.EndDate AS CostEnd, cpc.Id AS                           CustomCostID, cpc.ItemName AS CustomItem, cpc.CostPerUnit AS CustomCPU,
                                               cpc.Quantity AS CustomQuantity, cpc.DateUsed AS CustomDate, cpc.UnitOfMeasure AS                  CustomUnits, cpc.Category AS CustomCategory
                                          FROM Project p
                                     LEFT JOIN Customer c ON p.CustomerId = c.Id
                                     LEFT JOIN ProjectCost pc ON p.Id = pc.ProjectId
                                     LEFT JOIN CustomProjectCost cpc ON p.Id = cpc.ProjectId
                                     LEFT JOIN CostItem ci ON pc.CostItemId = ci.Id
                                     LEFT JOIN CostPerUnit cpu ON pc.CostPerUnitId = cpu.Id
                                     LEFT JOIN CostCategory cc ON ci.CostCategoryId = cc.Id
                                     LEFT JOIN UnitOfMeasure um ON ci.UnitOfMeasureId = um.Id 
                                         WHERE p.Id = @id
                                      ORDER BY pc.DateUsed DESC;";

                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    SqlDataReader reader = cmd.ExecuteReader();

                    Project project = null;

                    while (reader.Read()) {

                        if (project == null) {

                            project = new Project {

                                Id = reader.GetInt32(reader.GetOrdinal("ProjectId")),
                                ProjectName = reader.GetString(reader.GetOrdinal("ProjectName")),
                                StartDate = reader.GetDateTime(reader.GetOrdinal("ProjectStart")),
                                IsComplete = reader.GetBoolean(reader.GetOrdinal("IsComplete")),
                                Customer = new Customer(),
                                ProjectCosts = new List<ProjectCost>(),
                                CustomCosts = new List<CustomProjectCost>()
                            };
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("ProjectEnd"))) {

                            project.CompletionDate = reader.GetDateTime(reader.GetOrdinal("ProjectEnd"));
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("AmountCharged"))) {

                            project.AmountCharged = reader.GetDouble(reader.GetOrdinal("AmountCharged"));
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("CustomerId"))) {

                            project.Customer.Id = reader.GetInt32(reader.GetOrdinal("CustomerId"));
                            project.Customer.FirstName = reader.GetString(reader.GetOrdinal("FirstName"));
                            project.Customer.LastName = reader.GetString(reader.GetOrdinal("LastName"));
                            project.Customer.Email = reader.GetString(reader.GetOrdinal("Email"));
                            project.Customer.PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber"));
                            
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("CustomCostId"))) {

                            int customCostId = reader.GetInt32(reader.GetOrdinal("CustomCostId"));

                            if (!project.CustomCosts.Any(cpc => cpc.Id == customCostId)) {

                                CustomProjectCost customCost = new CustomProjectCost {
                                    Id = reader.GetInt32(reader.GetOrdinal("CustomCostId")),
                                    ItemName = reader.GetString(reader.GetOrdinal("CustomItem")),
                                    CostPerUnit = reader.GetDouble(reader.GetOrdinal("CustomCPU")),
                                    Quantity = reader.GetInt32(reader.GetOrdinal("CustomQuantity")),
                                    DateUsed = reader.GetDateTime(reader.GetOrdinal("CustomDate")),
                                    UnitOfMeasure = reader.GetString(reader.GetOrdinal("CustomUnits")),
                                    Category = reader.GetString(reader.GetOrdinal("CustomCategory"))
                                };
                                project.CustomCosts.Add(customCost);
                            }
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("ProjectCostId"))) {

                            int projectCostId = reader.GetInt32(reader.GetOrdinal("ProjectCostId"));

                            if (!project.ProjectCosts.Any(pc => pc.Id == projectCostId)) {

                                ProjectCost projectCost = new ProjectCost {

                                    Id = reader.GetInt32(reader.GetOrdinal("ProjectCostId")),
                                    DateUsed = reader.GetDateTime(reader.GetOrdinal("DateUsed")),
                                    Quantity = reader.GetDouble(reader.GetOrdinal("Quantity")),
                                    CostItem = new CostItem {
                                        Id = reader.GetInt32(reader.GetOrdinal("CostItemId")),
                                        ItemName = reader.GetString(reader.GetOrdinal("ItemName")),
                                        UnitOfMeasure = new UnitOfMeasure {
                                            Id = reader.GetInt32(reader.GetOrdinal("UnitId")),
                                            UnitName = reader.GetString(reader.GetOrdinal("UnitName"))
                                        },
                                        CostCategory = new CostCategory {
                                            Id = reader.GetInt32(reader.GetOrdinal("CostCategoryId")),
                                            CategoryName = reader.GetString(reader.GetOrdinal("CategoryName"))
                                        }
                                    },
                                    CostPerUnit = new CostPerUnit {
                                        Id = reader.GetInt32(reader.GetOrdinal("CostPerUnitId")),
                                        Cost = reader.GetDouble(reader.GetOrdinal("Cost"))
                                    }
                                };
                                project.ProjectCosts.Add(projectCost);
                            }
                        }
                    }
                    reader.Close();
                    return project;
                }
            }
        }
    }
}
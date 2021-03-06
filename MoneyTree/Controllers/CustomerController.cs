﻿using System;
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

    public class CustomerController : Controller {

        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;

        public CustomerController(ApplicationDbContext context, IConfiguration config) {
            _context = context;
            _config = config;
        }

        // GET: Customers
        public async Task<IActionResult> Index() {

            CustomerIndexViewModel viewModel = new CustomerIndexViewModel(); 

               var CustomerList = await (
                    from Customer in _context.Customer
                    group Customer by Customer.LastName.Substring(0, 1) into customerGroup
                    select new GroupedCustomers {
                        FirstLetter = customerGroup.Key,
                        CustomerList = customerGroup.ToList()
                    }).OrderBy(letter => letter.FirstLetter)
                    .ToListAsync();

            viewModel.GroupedCustomers = CustomerList;
            return View(viewModel);
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id) {

            ProjectController projectController = new ProjectController(_context, _config);
            EstimateController estimateController = new EstimateController(_context, _config);

            if (id == null) {
                return NotFound();
            }

            DateTime Today = DateTime.UtcNow;

            CustomerDetailViewModel viewModel = new CustomerDetailViewModel {

                Customer = await _context.Customer.FirstOrDefaultAsync(c => c.Id == id),
                CustomerCurrentProjects = await _context.Project.Where(p => p.CustomerId == id && p.IsComplete == false).ToListAsync(),
                CustomerCompletedProjects = await _context.Project.Where(p => p.CustomerId == id && p.IsComplete == true).ToListAsync(),
                CustomerCurrentEstimates = await _context.Estimate.Where(e=> e.CustomerId == id && e.ExpirationDate <= Today).ToListAsync(),
                CustomerPastEstimates = await _context.Estimate.Where(e => e.CustomerId == id && Today > e.ExpirationDate ).ToListAsync()
            };


            if (viewModel.Customer == null) {
                return NotFound();
            }

            return View(viewModel);
        }

        // GET: Customers/Create
        public IActionResult Create() {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,PhoneNumber,Email")] Customer customer) {

            if (ModelState.IsValid) {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return NotFound();
            }

            var customer = await _context.Customer.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,PhoneNumber,Email")] Customer customer) {

            if (id != customer.Id) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {

                    if (!CustomerExists(customer.Id)) {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        private bool CustomerExists(int id) {
            return _context.Customer.Any(e => e.Id == id);
        }
    }
}

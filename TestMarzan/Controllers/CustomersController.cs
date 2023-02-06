using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestMarzan.Data;
using TestMarzan.Interfaces;
using TestMarzan.Models;

namespace TestMarzan.Controllers
{
    /// <summary>
    /// This class represet controller of Customer
    /// </summary>
    public class CustomersController : Controller
    {
        private readonly ICustomer _customerServices;

        /// <param name="customerServices"><see cref="ICustomer"/></param>
        public CustomersController(ICustomer customerServices)
        {
            _customerServices = customerServices;
        }

        // GET: Customers
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            return View(await _customerServices.GetCustomers(searchString));
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            return await CustomerExists(id);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName,CardNumber,BirthDate")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                await _customerServices.CreateCustomer(customer);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            return await CustomerExists(id);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,FullName,CardNumber,BirthDate")] Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _customerServices.DeleteCustomer(id);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            return await CustomerExists(id);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _customerServices.DeleteCustomer(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<IActionResult> CustomerExists(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Customer customer =await _customerServices.GetCustomer(id);

            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }
    }
}

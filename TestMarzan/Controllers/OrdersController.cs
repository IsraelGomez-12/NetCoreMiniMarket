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
using TestMarzan.ViewModel;

namespace TestMarzan.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ContextDb _context;
        private readonly IOrder _orderServices;

        /// <param name="customerServices"><see cref="IOrder"/></param>
        public OrdersController(IOrder orderServices, ContextDb context)
        {
            _orderServices = orderServices;
            _context = context;

        }

        // GET: Orders
        public async Task<IActionResult> Index(string searchString)
        {
            OrderViewModel viewModel = new OrderViewModel();

            viewModel.orders = await _orderServices.GetOrders(searchString);
            viewModel.Products = await _context.Products.ToListAsync();
            viewModel.Customers = await _context.Customers.ToListAsync();
            ViewData["CurrentFilter"] = searchString;

            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(List<Guid> productList, [FromForm(Name = "CostumerId")] Guid CostumerId)
        {
            if (ModelState.IsValid)
            {
                await _orderServices.CreateOrder(productList, CostumerId);
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction("Index", "Orders");
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
			var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            var or = new EditOrderView();
            or.Products = await _context.Products.ToListAsync();
            or.Customers = await _context.Customers.ToListAsync();
            or.order = order;
             
			return View(or);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
       // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,Order order, List<Guid> productList)
        {

            if (order != null)
            {

                await _orderServices.EditOrder(id,productList);

                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction("Index", "Orders");
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _orderServices.DeleteOrder(id);
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(Guid id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}

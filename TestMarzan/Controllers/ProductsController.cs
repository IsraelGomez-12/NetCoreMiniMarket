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
    /// This Class represent controller of Product
    /// </summary>
    public class ProductsController : Controller
    {
        private readonly IProduct _productServices;

        /// <param name="productServices"<see cref="IProduct"/>></param>
        public ProductsController(IProduct productServices)
        {
            _productServices = productServices;
        }

        // GET: Products
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            return View(await _productServices.GetProducts(searchString));
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            return await ProducExists(id);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Type,Price,ExpirationDate")] Product product)
        {
            if (ModelState.IsValid)
            {
                await _productServices.CreateProduct(product);               
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            return await ProducExists(id);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Type,Price,ExpirationDate")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                
                await _productServices.EditProduct(product);
                
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            return await ProducExists(id);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _productServices.DeleteProduct(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<IActionResult> ProducExists(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product product = await _productServices.GetProduct(id);

            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

    }
}

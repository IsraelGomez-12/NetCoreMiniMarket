using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMarzan.Data;
using TestMarzan.Interfaces;
using TestMarzan.Models;

namespace TestMarzan.Services
{
    /// <summary>
    /// This class Represent Product data and all logic management.
    /// </summary>
    public class ProductServices : IProduct
    {
        /// <summary>
        /// An instace of <see cref="ContextDb"/>
        /// </summary>
        private readonly ContextDb _contextDb;

        /// <summary>
        /// Initialize a new instace of <see cref="ProductServices"/>
        /// </summary>
        /// <param name="contextDb"></param>
        public ProductServices(ContextDb contextDb)
        {
            _contextDb = contextDb;
        }
        
        ///<inheritdoc/>
        public async Task<List<Product>> GetProducts(string text)
        {
            List<Product> products = new List<Product>();
            if (text != null)
            {
                products = await _contextDb.Products.Where(product => product.Name.Contains(text) || product.Type.Contains(text)).ToListAsync();
            }
            else
            {
                products = await _contextDb.Products.ToListAsync();
            }

            return products;
        }

        ///<inheritdoc/>
        public async Task<Product> GetProduct(Guid? id)
        {
            Product productToShow = await _contextDb.Products.FirstOrDefaultAsync(product => product.Id == id);
            return productToShow;
        }

        ///<inheritdoc/>
        public async Task<Product> CreateProduct(Product product)
        {
            product.Id = Guid.NewGuid();
            await _contextDb.Products.AddAsync(product);
            await _contextDb.SaveChangesAsync();
            return product;
        }

        ///<inheritdoc/>
        public async Task<Product> EditProduct(Product product)
        {
            _contextDb.Products.Update(product);
            await _contextDb.SaveChangesAsync();
            return product;
        }

        public async Task<Product> DeleteProduct(Guid id)
        {
            Product productToDelete =await _contextDb.Products.FirstOrDefaultAsync(product => product.Id == id);
            _contextDb.Products.Remove(productToDelete);
            await _contextDb.SaveChangesAsync();
            return productToDelete;
        }
    }
}

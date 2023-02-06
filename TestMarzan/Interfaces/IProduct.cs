using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMarzan.Models;

namespace TestMarzan.Interfaces
{
    /// <summary>
    /// This interface is to Product Services
    /// </summary>
    public interface IProduct
    {
        /// <summary>
        /// Get products by parameter
        /// </summary>
        /// <param name="text">represent Name or Type</param>
        /// <returns>A list of <see cref="Product"/></returns>
        Task<List<Product>> GetProducts(string text);

        /// <summary>
        /// Get product by id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>an instance of <see cref="Product"/></returns>
        Task<Product> GetProduct(Guid? id);

        /// <summary>
        /// Create Product on database 
        /// </summary>
        /// <param name="product"></param>
        /// <returns>an instance of <see cref="Product"/></returns>
        Task<Product> CreateProduct(Product product);

        /// <summary>
        /// Edit product on database
        /// </summary>
        /// <param name="product"></param>
        /// <returns>an instance of <see cref="Product"/></returns>
        Task<Product> EditProduct(Product product);

        /// <summary>
        /// Delete product on database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>an instance of <see cref="Product"/></returns>
        Task<Product> DeleteProduct(Guid id);
    }
}

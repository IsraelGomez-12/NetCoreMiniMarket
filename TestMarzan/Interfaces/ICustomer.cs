using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMarzan.Models;

namespace TestMarzan.Interfaces
{
    /// <summary>
    /// This Interface for Customer Services
    /// </summary>
    public interface ICustomer
    {
        /// <summary>
        /// Get All Customers by parameter
        /// </summary>
        /// <param name="text">Full Name of Customer</param>
        /// <returns>A list of <see cref="Customer"/></returns>
        Task<List<Customer>> GetCustomers(string text);

        /// <summary>
        /// Get Customer by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>an instance of <see cref="Customer"/></returns>
        Task<Customer> GetCustomer(Guid? id);

        /// <summary>
        /// Create Customer on database 
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>an instance of <see cref="Customer"/></returns>
        Task<Customer> CreateCustomer(Customer customer);

        /// <summary>
        /// Edit Customer on database
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>an instance of <see cref="Customer"/></returns>
        Task<Customer> EditCustomer(Customer customer);

        /// <summary>
        /// Delete Customer on database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>an instance of <see cref="Customer"/></returns>
        Task<Customer> DeleteCustomer(Guid id);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMarzan.Models;
using TestMarzan.ViewModel;

namespace TestMarzan.Interfaces
{
    public interface IOrder
    {
        /// <summary>
        /// Get All Customers by parameter
        /// </summary>
        /// <param name="text">Full Name of Customer</param>
        /// <returns>A list of <see cref="Customer"/></returns>
        Task<List<OrderView>> GetOrders(string text);

        /// <summary>
        /// Get Customer by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>an instance of <see cref="Customer"/></returns>
        Task<Order> GetCustomer(Guid? id);

        /// <summary>
        /// Create Customer on database 
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="productList"></param>
        /// <returns>an instance of <see cref="Order"/></returns>
        Task<Order> CreateOrder(List<Guid> productList, Guid customerId);

        /// <summary>
        /// Edit Customer on database
        /// </summary>
        /// <param name="order"></param>
        /// <returns>an instance of <see cref="Order"/></returns>
        Task<Order> EditOrder(Guid id, List<Guid> productList);

        /// <summary>
        /// Delete Customer on database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>an instance of <see cref="Order"/></returns>
        Task<Order> DeleteOrder(Guid id);
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestMarzan.Data;
using TestMarzan.Interfaces;
using TestMarzan.Models;
using TestMarzan.ViewModel;

namespace TestMarzan.Services
{
    /// <summary>
    /// This class represent order data and all logic management.
    /// </summary>
    public class OrderService : IOrder
    {
        /// <summary>
        /// An instance of <see cref="ContextDb"/>
        /// </summary>
        private readonly ContextDb _contextDb;

        private Order orderNumber = new Order();

        /// <summary>
        /// Initialize a new instance of <see cref="OrderService"/>
        /// </summary>
        /// <param name="contextDb"></param>
        public OrderService(ContextDb contextDb)
        {
            _contextDb = contextDb;
        }

        ///<inheritdoc/>
        public async Task<Order> CreateOrder(List<Guid> productList, Guid customerId)
        {
            Order order = new Order();

            if (productList.Count() == 0)
                return order;

            order.ProductCount = productList.Count();
            order.CostumerId = customerId.ToString();
           
            foreach(Guid product in productList)
            {
                Product item = await _contextDb.Products.FirstOrDefaultAsync(x=>x.Id == product);

                order.Products += item.Name + ", ";
                order.TotalPrice += item.Price;
            }
           
            await _contextDb.Orders.AddAsync(order);
            await _contextDb.SaveChangesAsync();

            return order;
        }

        ///<inheritdoc/>
        public async Task<Order> DeleteOrder(Guid id)
        {
            Order orderToDelete = await _contextDb.Orders.FirstOrDefaultAsync(order => order.Id == id);
            _contextDb.Orders.Remove(orderToDelete);
            await _contextDb.SaveChangesAsync();
            return orderToDelete;
        }

        public async Task<Order> EditOrder(Guid id, List<Guid> productList)
        {
            Order orderToUpdate = await _contextDb.Orders.FirstOrDefaultAsync(order => order.Id == id);

            orderToUpdate.ProductCount = productList.Count();

            orderToUpdate.Products = string.Empty;
            orderToUpdate.TotalPrice = 0;

            foreach (Guid product in productList)
            {
                Product item = await _contextDb.Products.FirstOrDefaultAsync(x => x.Id == product);

                orderToUpdate.Products += item.Name + ", ";
                orderToUpdate.TotalPrice += item.Price;
            }

            _contextDb.Orders.Update(orderToUpdate);
            await _contextDb.SaveChangesAsync();

            return orderToUpdate;
        }

        public Task<Order> GetCustomer(Guid? id)
        {
            throw new NotImplementedException();
        }

        ///<inheritdoc/>
        public async Task<List<OrderView>> GetOrders(string text)
        {
            OrderView orderView = new OrderView();
            List<OrderView> listorderView = new List<OrderView>();
            List<Order> orders = new List<Order>();

            if (text == null)
            {
                orders = await _contextDb.Orders.ToListAsync();
            }
            else
            {
                orders = await _contextDb.Orders.Where(x => x.Products.Contains(text)).ToListAsync();
            }
            
            foreach(Order order in orders)
            {
                Customer customer = await _contextDb.Customers.FirstOrDefaultAsync(x => x.Id.ToString() == order.CostumerId);
                orderView.Id = order.Id;
                orderView.NameCustomer = customer.FullName;
                orderView.ProductCount = order.ProductCount;
                orderView.Products = order.Products;
                orderView.TotalPrice = order.TotalPrice;

                listorderView.Add(orderView);
                orderView = new OrderView();
            }

            return listorderView;
        }
    }
}

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
    /// This class represent customer data and all logic management.
    /// </summary>
    public class CustomerServices : ICustomer
    {
        /// <summary>
        /// An instance of <see cref="ContextDb"/>
        /// </summary>
        private readonly ContextDb _contextDb;

        /// <summary>
        /// Initialize a new instance of <see cref="CustomerServices"/>
        /// </summary>
        /// <param name="contextDb"></param>
        public CustomerServices(ContextDb contextDb)
        {
            _contextDb = contextDb;
        }

        ///<inheritdoc/>
        public async Task<List<Customer>> GetCustomers(string text)
        {
            List<Customer> customers = new List<Customer>();
            if (text != null)
            {
                customers = await _contextDb.Customers.Where(customer => customer.FullName.Contains(text) || customer.CardNumber.Contains(text)).ToListAsync();
            }
            else
            {
                customers = await _contextDb.Customers.ToListAsync();
            }
            return customers;
        }

        ///<inheritdoc/>
        public async Task<Customer> GetCustomer(Guid? id)
        {
            Customer customerToShow = await _contextDb.Customers.FirstOrDefaultAsync(customer => customer.Id == id);
            return customerToShow;
        }

        ///<inheritdoc/>
        public async Task<Customer> CreateCustomer(Customer customer)
        {
            customer.Id = Guid.NewGuid();
            await _contextDb.Customers.AddAsync(customer);
            await _contextDb.SaveChangesAsync();
            return customer;
        }

        ///<inheritdoc/>
        public async Task<Customer> EditCustomer(Customer customer)
        {
            _contextDb.Customers.Update(customer);
            await _contextDb.SaveChangesAsync();
            return customer;
        }

        ///<inheritdoc/>
        public async Task<Customer> DeleteCustomer(Guid id)
        {
            Customer customerToDelete = await _contextDb.Customers.FirstOrDefaultAsync(customer => customer.Id == id);
            _contextDb.Customers.Remove(customerToDelete);
            await _contextDb.SaveChangesAsync();
            return customerToDelete;
        }
    }
}

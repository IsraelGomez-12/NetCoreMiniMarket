using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMarzan.Models;

namespace TestMarzan.ViewModel
{
    public class OrderViewModel
    {
        public List<OrderView> orders { get; set; }
        public List<Product> Products { get; set; }
        public List<Customer> Customers { get; set; }
        public int CostumerId { get; set; }
    }
}

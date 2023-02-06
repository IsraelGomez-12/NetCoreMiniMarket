using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestMarzan.ViewModel
{
    public class OrderView
    {
        public Guid Id { get; set; }
        public int OrderNumber { get; set; }
        public string NameCustomer { get; set; }
        public int ProductCount { get; set; }
        public string Products { get; set; }
        public int TotalPrice { get; set; }
    }
}

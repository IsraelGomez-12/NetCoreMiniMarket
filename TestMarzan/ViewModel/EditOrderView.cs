using System.Collections.Generic;
using TestMarzan.Models;

namespace TestMarzan.ViewModel
{
    public class EditOrderView
    {
		public Order order { get; set; } = new Order();
		public List<Product> Products { get; set; }
		public List<Customer> Customers { get; set; }
		//public int CostumerId { get; set; }
	}
}

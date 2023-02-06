using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestMarzan.Models
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string CostumerId { get; set; }
        public Customer Customer { get; set; }
        [Required]
        public int ProductCount { get; set; }
        [Required]
        public string Products { get; set; }
        [Required]
        public int TotalPrice { get; set; }
    }
}

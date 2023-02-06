using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestMarzan.Models
{
    public class Customer
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string FullName { get; set; }
        [Required]
        public string CardNumber { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}

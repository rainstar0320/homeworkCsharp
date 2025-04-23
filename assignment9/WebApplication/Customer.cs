using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OrderManagement.Models
{
    public class Customer
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
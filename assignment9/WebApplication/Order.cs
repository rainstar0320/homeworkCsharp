// Models/Order.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderManagement.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Required]
        [ForeignKey(nameof(Customer))]
        public string CustomerId { get; set; }

        [Required]
        public DateTime CreateTime { get; set; } = DateTime.UtcNow;

        // 导航属性
        public virtual Customer Customer { get; set; }
        public virtual ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();

        // DTO映射时需要忽略的字段
        [NotMapped]
        public string CustomerName => Customer?.Name;

        [NotMapped]
        public decimal TotalPrice => Items?.Sum(i => i.Quantity * i.Product.Price) ?? 0;

        public void AddItem(OrderItem item)
        {
            if (Items.Any(i => i.ProductId == item.ProductId))
                throw new InvalidOperationException($"Product {item.ProductId} already exists in order");

            Items.Add(item);
        }
    }
}
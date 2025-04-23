// Models/DTO/OrderDTO.cs
using System.ComponentModel.DataAnnotations;

namespace OrderManagement.Models.DTO
{
    public class OrderCreateDTO
    {
        [Required]
        public string CustomerId { get; set; }

        [Required]
        [MinLength(1)]
        public List<OrderItemCreateDTO> Items { get; set; }
    }

    public class OrderResponseDTO
    {
        public string Id { get; set; }
        public DateTime CreateTime { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderItemResponseDTO> Items { get; set; }
    }
}

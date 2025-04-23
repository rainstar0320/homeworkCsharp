// Services/IOrderService.cs
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Services;

namespace OrderManagement.Services
{
    public interface IOrderService
    {
        Task<Order> AddOrder(Order order);
        Task RemoveOrder(int orderId);
        Task<IEnumerable<Order>> QueryOrders(string? productName, string? customerName);
        Task ImportOrders(Stream xmlStream);
        Task<Stream> ExportOrders();
    }
}
// Controllers/OrdersController.cs
using Microsoft.AspNetCore.Mvc;

namespace OrderManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(Order order)
        {
            var createdOrder = await _orderService.AddOrder(order);
            return CreatedAtAction(nameof(GetOrder),
                new { id = createdOrder.Id }, createdOrder);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            // 实现详情查询逻辑
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await _orderService.RemoveOrder(id);
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> QueryOrders(
            [FromQuery] string? product,
            [FromQuery] string? customer)
        {
            var orders = await _orderService.QueryOrders(product, customer);
            return Ok(orders);
        }

        [HttpPost("import")]
        public async Task<IActionResult> ImportOrders(IFormFile file)
        {
            using var stream = file.OpenReadStream();
            await _orderService.ImportOrders(stream);
            return Ok();
        }

        [HttpGet("export")]
        public async Task<IActionResult> ExportOrders()
        {
            var stream = await _orderService.ExportOrders();
            return File(stream, "application/xml", "orders.xml");
        }
    }
}
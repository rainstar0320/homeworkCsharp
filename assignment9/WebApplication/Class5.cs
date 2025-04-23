// Services/OrderService.cs
using OrderManagement.Data;
using OrderManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Xml.Serialization;

namespace OrderManagement.Services
{
    public class OrderService : IOrderService, IDisposable
    {
        private readonly AppDbContext _context;
        private readonly XmlSerializer _serializer = new(typeof(List<Order>));

        public OrderService(AppDbContext context)
        {
            _context = context;
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            if (!_context.Products.Any())
            {
                _context.Products.AddRange(
                    new Product { Name = "apple", Price = 100.0m },
                    new Product { Name = "egg", Price = 200.0m }
                );
            }

            if (!_context.Customers.Any())
            {
                _context.Customers.AddRange(
                    new Customer { Name = "li" },
                    new Customer { Name = "zhang" }
                );
            }
            _context.SaveChanges();
        }

        public async Task<Order> AddOrderAsync(Order order)
        {
            FixOrderReferences(order);
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task RemoveOrderAsync(string orderId)
        {
            var order = await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order != null)
            {
                _context.OrderItems.RemoveRange(order.Items);
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Order>> QueryOrdersAsync(
            string? productName,
            string? customerName,
            decimal? minTotalPrice)
        {
            var query = _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                .AsQueryable();

            if (!string.IsNullOrEmpty(customerName))
                query = query.Where(o => o.Customer.Name == customerName);

            if (!string.IsNullOrEmpty(productName))
                query = query.Where(o => o.Items.Any(i =>
                    i.Product.Name.Contains(productName)));

            if (minTotalPrice.HasValue)
                query = query.Where(o =>
                    o.Items.Sum(i => i.Quantity * i.Product.Price) > minTotalPrice);

            return await query.ToListAsync();
        }

        public async Task ImportOrdersAsync(Stream xmlStream)
        {
            var orders = (List<Order>)_serializer.Deserialize(xmlStream);

            foreach (var order in orders)
            {
                if (!await _context.Orders.AnyAsync(o => o.Id == order.Id))
                {
                    FixOrderReferences(order);
                    _context.Orders.Add(order);
                }
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Stream> ExportOrdersAsync()
        {
            var orders = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                .ToListAsync();

            var stream = new MemoryStream();
            _serializer.Serialize(stream, orders);
            stream.Position = 0;
            return stream;
        }

        private static void FixOrderReferences(Order order)
        {
            order.CustomerId = order.Customer?.Id;
            order.Customer = null;

            foreach (var item in order.Items)
            {
                item.ProductId = item.Product?.Id;
                item.Product = null;
                item.Order = null; // 防止循环引用
            }
        }

        public void Dispose() => _context?.Dispose();
    }
}

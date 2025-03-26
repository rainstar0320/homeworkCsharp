using System.ComponentModel;

namespace WinForms3_26
{

    // OrderServiceLib项目中的类
    public class Order
    {
        public int OrderId { get; set; }
        public string Customer { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public BindingList<OrderDetail> Details { get; } = new BindingList<OrderDetail>();

        public decimal TotalAmount => Details.Sum(d => d.Price * d.Quantity);

        public Order Clone()
        {
            var newOrder = new Order
            {
                OrderId = this.OrderId,
                Customer = this.Customer,
                OrderDate = this.OrderDate
            };
            foreach (var detail in Details)
            {
                newOrder.Details.Add(detail.Clone());
            }
            return newOrder;
        }
    }

    public class OrderDetail
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public OrderDetail Clone()
        {
            return new OrderDetail
            {
                ProductName = this.ProductName,
                Price = this.Price,
                Quantity = this.Quantity
            };
        }
    }

    public class OrderService
    {
        private List<Order> orders = new List<Order>();
        private int nextOrderId = 1;

        public void AddOrder(Order order)
        {
            if (orders.Any(o => o.OrderId == order.OrderId))
                throw new ArgumentException("Order ID already exists");

            order.OrderId = nextOrderId++;
            orders.Add(order);
        }

        public void RemoveOrder(int orderId)
        {
            var order = GetOrder(orderId);
            if (order != null)
                orders.Remove(order);
        }

        public void UpdateOrder(Order newOrder)
        {
            var existing = GetOrder(newOrder.OrderId);
            if (existing == null)
                throw new ArgumentException("Order not found");

            orders.Remove(existing);
            orders.Add(newOrder);
        }

        public List<Order> QueryOrders(string keyword = "")
        {
            var query = orders.AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(o =>
                    o.Customer.Contains(keyword) ||
                    o.OrderId.ToString().Contains(keyword) ||
                    o.Details.Any(d => d.ProductName.Contains(keyword))
                );
            }
            return query.OrderByDescending(o => o.OrderDate).ToList();
        }

        public Order GetOrder(int orderId) => orders.FirstOrDefault(o => o.OrderId == orderId);
    }
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

public class Order : IEquatable<Order>
{
    public int OrderId { get; }
    public string Customer { get; set; }
    public List<OrderDetails> Details { get; } = new List<OrderDetails>();
    public decimal TotalAmount => Details.Sum(d => d.Quantity * d.Price);

    public Order(int orderId, string customer, IEnumerable<OrderDetails> details)
    {
        OrderId = orderId;
        Customer = customer;
        foreach (var detail in details.Distinct())
        {
            if (Details.Contains(detail))
                throw new ArgumentException("订单明细重复。");
            Details.Add(detail);
        }
    }

    public bool Equals(Order other) => other != null && OrderId == other.OrderId;

    public override bool Equals(object obj) => Equals(obj as Order);

    public override int GetHashCode() => OrderId.GetHashCode();

    public override string ToString() =>
        $"订单号：{OrderId}, 客户：{Customer}, 总金额：{TotalAmount:C}\n" +
        string.Join("\n", Details.Select(d => d.ToString()));
}

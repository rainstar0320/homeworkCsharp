using System;
using System.Collections.Generic;
using System.Linq;

public class OrderService
{
    private List<Order> _orders = new List<Order>();

    public void AddOrder(Order order)
    {
        if (_orders.Any(o => o.Equals(order)))
            throw new InvalidOperationException("订单已存在。");
        _orders.Add(order);
    }

    public void RemoveOrder(int orderId)
    {
        var order = _orders.FirstOrDefault(o => o.OrderId == orderId);
        if (order == null)
            throw new InvalidOperationException("订单不存在。");
        _orders.Remove(order);
    }

    public void UpdateOrder(Order newOrder)
    {
        var index = _orders.FindIndex(o => o.OrderId == newOrder.OrderId);
        if (index == -1)
            throw new InvalidOperationException("订单不存在。");
        _orders[index] = newOrder;
    }

    public IEnumerable<Order> QueryByOrderId(int orderId) =>
        _orders.Where(o => o.OrderId == orderId).OrderBy(o => o.TotalAmount);

    public IEnumerable<Order> QueryByProductName(string productName) =>
        _orders.Where(o => o.Details.Any(d => d.ProductName == productName))
              .OrderBy(o => o.TotalAmount);

    public IEnumerable<Order> QueryByCustomer(string customer) =>
        _orders.Where(o => o.Customer == customer).OrderBy(o => o.TotalAmount);

    public IEnumerable<Order> QueryByAmountRange(decimal min, decimal max) =>
        _orders.Where(o => o.TotalAmount >= min && o.TotalAmount <= max)
              .OrderBy(o => o.TotalAmount);

    public void Sort() => _orders = _orders.OrderBy(o => o.OrderId).ToList();

    public void SortBy(Func<Order, IComparable> keySelector) =>
        _orders = _orders.OrderBy(keySelector).ToList();
}
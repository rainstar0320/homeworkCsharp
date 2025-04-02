using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

[TestClass]
public class OrderServiceTests
{
    [TestMethod]
    public void AddOrder_UniqueOrder_ShouldAddSuccessfully()
    {
        var service = new OrderService();
        var order = new Order(1, "Alice", new[] { new OrderDetails { ProductName = "Book", Quantity = 2, Price = 30 } });
        service.AddOrder(order);
        Assert.AreEqual(1, service.QueryByOrderId(1).Count());
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void AddOrder_DuplicateOrderId_ShouldThrow()
    {
        var service = new OrderService();
        service.AddOrder(new Order(1, "Alice", new OrderDetails[0]));
        service.AddOrder(new Order(1, "Bob", new OrderDetails[0]));
    }

    [TestMethod]
    public void QueryByProduct_ShouldReturnCorrectOrders()
    {
        var service = new OrderService();
        service.AddOrder(new Order(1, "Alice", new[] { new OrderDetails { ProductName = "Apple", Quantity = 5, Price = 10 } }));
        service.AddOrder(new Order(2, "Bob", new[] { new OrderDetails { ProductName = "Banana", Quantity = 3, Price = 5 } }));

        var results = service.QueryByProductName("Apple").ToList();
        Assert.AreEqual(1, results.Count);
        Assert.AreEqual("Alice", results[0].Customer);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void RemoveOrder_NonExistingOrder_ShouldThrow()
    {
        var service = new OrderService();
        service.RemoveOrder(999);
    }
}

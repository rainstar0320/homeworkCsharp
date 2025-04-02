using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        var service = new OrderService();
        while (true)
        {
            Console.WriteLine("\n1.添加订单 2.删除订单 3.修改订单 4.查询订单 5.退出");
            Console.Write("请选择操作：");
            switch (Console.ReadLine())
            {
                case "1": AddOrder(service); break;
                case "2": RemoveOrder(service); break;
                case "3": UpdateOrder(service); break;
                case "4": QueryOrders(service); break;
                case "5": return;
            }
        }
    }

    static void AddOrder(OrderService service)
    {
        try
        {
            var (id, customer, details) = ReadOrderData();
            service.AddOrder(new Order(id, customer, details));
            Console.WriteLine("添加成功！");
        }
        catch (Exception e) { Console.WriteLine($"错误：{e.Message}"); }
    }

    static void RemoveOrder(OrderService service)
    {
        Console.Write("输入订单号：");
        try { service.RemoveOrder(int.Parse(Console.ReadLine())); }
        catch (Exception e) { Console.WriteLine($"错误：{e.Message}"); }
    }

    static void UpdateOrder(OrderService service)
    {
        try
        {
            Console.Write("输入原订单号：");
            int id = int.Parse(Console.ReadLine());
            var (_, customer, details) = ReadOrderData();
            service.UpdateOrder(new Order(id, customer, details));
            Console.WriteLine("修改成功！");
        }
        catch (Exception e) { Console.WriteLine($"错误：{e.Message}"); }
    }

    static (int id, string customer, List<OrderDetails> details) ReadOrderData()
    {
        Console.Write("订单号：");
        int id = int.Parse(Console.ReadLine());
        Console.Write("客户名：");
        string customer = Console.ReadLine();
        var details = new List<OrderDetails>();
        while (true)
        {
            Console.Write("添加商品？(y/n)：");
            if (Console.ReadLine().ToLower() != "y") break;
            Console.Write("商品名：");
            string product = Console.ReadLine();
            Console.Write("数量：");
            int qty = int.Parse(Console.ReadLine());
            Console.Write("单价：");
            decimal price = decimal.Parse(Console.ReadLine());
            details.Add(new OrderDetails { ProductName = product, Quantity = qty, Price = price });
        }
        return (id, customer, details);
    }

    static void QueryOrders(OrderService service)
    {
        Console.WriteLine("1.按订单号 2.按商品 3.按客户 4.按金额范围");
        switch (Console.ReadLine())
        {
            case "1":
                Console.Write("订单号：");
                Display(service.QueryByOrderId(int.Parse(Console.ReadLine())));
                break;
            case "2":
                Console.Write("商品名：");
                Display(service.QueryByProductName(Console.ReadLine()));
                break;
            case "3":
                Console.Write("客户名：");
                Display(service.QueryByCustomer(Console.ReadLine()));
                break;
            case "4":
                Console.Write("最小金额：");
                decimal min = decimal.Parse(Console.ReadLine());
                Console.Write("最大金额：");
                Display(service.QueryByAmountRange(min, decimal.Parse(Console.ReadLine())));
                break;
        }
    }

    static void Display(IEnumerable<Order> orders)
    {
        foreach (var order in orders)
            Console.WriteLine(order + "\n----------");
        Console.WriteLine(orders.Any() ? "" : "无结果");
    }
}

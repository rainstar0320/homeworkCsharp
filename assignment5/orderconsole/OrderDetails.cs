using System;

public class OrderDetails : IEquatable<OrderDetails>
{
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }

    public bool Equals(OrderDetails other)
    {
        if (other is null) return false;
        return ProductName == other.ProductName &&
               Quantity == other.Quantity &&
               Price == other.Price;
    }

    public override bool Equals(object obj) => Equals(obj as OrderDetails);

    public override int GetHashCode() => HashCode.Combine(ProductName, Quantity, Price);

    public override string ToString() =>
        $"商品：{ProductName}, 数量：{Quantity}, 单价：{Price:C}, 小计：{Quantity * Price:C}";
}
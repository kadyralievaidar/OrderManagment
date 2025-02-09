using OrderManagment.Feautures.Products.Models;
using OrderManagment.Models;
using System.Collections.ObjectModel;

namespace OrderManagment.Feautures.Orders.Models;

/// <summary>
///     Order model
/// </summary>
public class Order : BaseModel
{
    /// <summary>
    ///     Prefered notifier of order
    /// </summary>
    public PreferedNotifier PreferedNotifier { get; set; } 
    /// <summary>
    ///     Order status
    /// </summary>
    public OrderStatus OrderStatus { get; set; }
    /// <summary>
    ///     Price of order
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    ///     Products of order
    /// </summary>
    public ICollection<Product> Products { get; set; } = new Collection<Product>();
}

public enum OrderStatus
{
    Failed,
    Pending,
    Completed
}
public enum PreferedNotifier
{
    Sms,
    Email
}

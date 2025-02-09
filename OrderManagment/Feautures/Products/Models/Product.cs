using OrderManagment.Feautures.Orders.Models;
using OrderManagment.Models;

namespace OrderManagment.Feautures.Products.Models;
public class Product : BaseModel
{
    /// <summary>
    ///     Price of product
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    ///     Id of Order
    /// </summary>
    public Guid OrderId { get; set; }

    /// <summary>
    ///     Order
    /// </summary>
    public Order Order { get; set; }
}

using OrderManagment.Feautures.Discount.Interfaces;

namespace OrderManagment.Feautures.Discount.Services;
public class NoDiscountService : IDiscountStrategy
{
    public decimal ApplyDiscount(decimal price)
    {
        return price;
    }
}

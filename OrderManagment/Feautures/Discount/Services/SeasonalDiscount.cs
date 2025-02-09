using OrderManagment.Feautures.Discount.Interfaces;

namespace OrderManagment.Feautures.Discount.Services;
public class SeasonalDiscount : IDiscountStrategy
{
    public decimal ApplyDiscount(decimal price)
    {
        return price * 0.5m;
    }
}

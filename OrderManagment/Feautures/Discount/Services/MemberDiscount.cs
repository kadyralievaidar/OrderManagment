using OrderManagment.Feautures.Discount.Interfaces;

namespace OrderManagment.Feautures.Discount.Services;
public class MemberDiscount : IDiscountStrategy
{
    public decimal ApplyDiscount(decimal price)
    {
        return price * 0.8m;
    }
}

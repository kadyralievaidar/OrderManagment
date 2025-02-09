namespace OrderManagment.Feautures.Discount.Interfaces;
public interface IDiscountStrategy
{
    decimal ApplyDiscount(decimal price);
}

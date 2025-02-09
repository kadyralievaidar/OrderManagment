using OrderManagment.Feautures.Payment.Interfaces;
using OrderManagment.Feautures.Payment.Services;

namespace OrderManagment.Feautures.Payment.Factory;
public class PaymentFactory
{
    public static IPayment CreatePayment(string paymentType)
    {
        return paymentType.ToLower() switch
        {
            "creditcard" => new CreditCardPayment(),
            "paypal" => new PayPalPayment(),
            "bitcoin" => new BitcoinPayment(),
            _ => throw new ArgumentException("Invalid payment type")
        };
    }
}

using OrderManagment.Feautures.Payment.Interfaces;

namespace OrderManagment.Feautures.Payment.Services;
public class BitcoinPayment : IPayment
{
    public int Pay(decimal amount)
    {
        var random = new Random();
        var mock = random.Next(1, 100);
        if(mock >= 50)
            return 1;

        return 0;
    }
}

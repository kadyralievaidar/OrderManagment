using OrderManagment.Feautures.Notification.Interfaces;
using OrderManagment.Feautures.Orders.Models;

namespace OrderManagment.Feautures.Notification.Services;
public class SMSNotifier : IObserver
{
    public int Update(OrderStatus orderStatus, Guid OrderId)
    {
        var random = new Random();
        var mock = random.Next(1,100);
        if (mock >= 50)
            return 1;

        return 0;
    }
}

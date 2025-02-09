using OrderManagment.Feautures.Payment.Models;

namespace OrderManagment.Database.UoW.Repositories;
public class PaymentRepository(OrderDbContext context) : GenericRepository<Payment>(context), IPaymentRepository
{
}

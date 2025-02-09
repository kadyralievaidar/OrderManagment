using OrderManagment.Feautures.Orders.Models;

namespace OrderManagment.Database.UoW.Repositories;
public class OrderRepository(OrderDbContext context) : GenericRepository<Order>(context), IOrderRepository
{
}

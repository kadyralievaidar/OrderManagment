using OrderManagment.Feautures.Products.Models;

namespace OrderManagment.Database.UoW.Repositories;
public class ProductRepository(OrderDbContext context) : GenericRepository<Product>(context), IProductRepository
{
}

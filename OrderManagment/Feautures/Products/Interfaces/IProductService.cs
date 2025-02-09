using OrderManagment.Feautures.Products.Models;
using System.Linq.Expressions;

namespace OrderManagment.Feautures.Products.Interfaces;
public interface IProductService
{
    Task AddProduct(Product product);
    Task DeleteProduct(Guid productId);
    Product GetProductById(Guid productId);
    IEnumerable<Product> GetProducts(Expression<Func<Product, bool>>? filter = null);
    Product? Update(Guid productId);
}

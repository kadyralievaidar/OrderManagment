using OrderManagment.Database.UoW.Repositories;
using OrderManagment.Feautures.Products.Interfaces;
using OrderManagment.Feautures.Products.Models;
using System.Linq.Expressions;

namespace OrderManagment.Feautures.Products.Services;
public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task AddProduct(Product product)
    {
        ArgumentNullException.ThrowIfNull(product);
        var exist = _repository.Any(x => x.Id == product.Id);
        if(!exist)
        {
            await _repository.AddAsync(product);
        }
    }

    public Task DeleteProduct(Guid productId)
    {
        var product = _repository.GetById(productId);
        if (product != null)
        {
            _repository.Remove(product);
        }
        return Task.CompletedTask;
    }

    public Product? GetProductById(Guid productId)
    {
        return _repository.GetById(productId);
    }

    public IEnumerable<Product> GetProducts(Expression<Func<Product, bool>>? filter = null)
    {
        return _repository.GetAllAsync(filter);
    }

    public Product? Update(Guid productId)
    {
        var product = _repository.GetById(productId);
        if (product != null)
        {
            _repository.Update(product);
        }
        return product;
    }
}

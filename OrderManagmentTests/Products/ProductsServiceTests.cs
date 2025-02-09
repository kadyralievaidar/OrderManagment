using Moq;
using OrderManagment.Database.UoW.Repositories;
using OrderManagment.Feautures.Products.Models;
using OrderManagment.Feautures.Products.Services;
using System.Linq.Expressions;

namespace OrderManagment.Tests.Feautures.Products;

[TestFixture]
public class ProductServiceTests
{
    private Mock<IProductRepository> _mockProductRepository;
    private ProductService _productService;

    [SetUp]
    public void SetUp()
    {
        _mockProductRepository = new Mock<IProductRepository>();
        _productService = new ProductService(_mockProductRepository.Object);
    }

    [Test]
    public async Task AddProduct_ShouldAddProduct_WhenProductDoesNotExist()
    {
        // Arrange
        var product = new Product { Id = Guid.NewGuid() };
        _mockProductRepository.Setup(repo => repo.Any(It.IsAny<Expression<Func<Product, bool>>>()))
                              .Returns(false);

        // Act
        await _productService.AddProduct(product);

        // Assert
        _mockProductRepository.Verify(repo => repo.AddAsync(It.Is<Product>(p => p.Id == product.Id)), Times.Once);
    }

    [Test]
    public async Task AddProduct_ShouldNotAddProduct_WhenProductExists()
    {
        // Arrange
        var product = new Product { Id = Guid.NewGuid() };
        _mockProductRepository.Setup(repo => repo.Any(It.IsAny<Expression<Func<Product, bool>>>()))
                              .Returns(true);

        // Act
        await _productService.AddProduct(product);

        // Assert
        _mockProductRepository.Verify(repo => repo.AddAsync(It.IsAny<Product>()), Times.Never);
    }

    [Test]
    public async Task DeleteProduct_ShouldRemoveProduct_WhenProductExists()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var product = new Product { Id = productId };
        _mockProductRepository.Setup(repo => repo.GetById(It.Is<Guid>(id => id == productId)))
                              .Returns(product);

        // Act
        await _productService.DeleteProduct(productId);

        // Assert
        _mockProductRepository.Verify(repo => repo.Remove(It.Is<Product>(p => p.Id == productId)), Times.Once);
    }

    [Test]
    public async Task DeleteProduct_ShouldNotRemoveProduct_WhenProductDoesNotExist()
    {
        // Arrange
        var productId = Guid.NewGuid();
        _mockProductRepository.Setup(repo => repo.GetById(It.Is<Guid>(id => id == productId)))
                              .Returns((Product)null);

        // Act
        await _productService.DeleteProduct(productId);

        // Assert
        _mockProductRepository.Verify(repo => repo.Remove(It.IsAny<Product>()), Times.Never);
    }

    [Test]
    public void GetProductById_ShouldReturnProduct_WhenProductExists()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var product = new Product { Id = productId };
        _mockProductRepository.Setup(repo => repo.GetById(It.Is<Guid>(id => id == productId)))
                              .Returns(product);

        // Act
        var result = _productService.GetProductById(productId);

        // Assert
        Assert.AreEqual(product, result);
    }

    [Test]
    public void GetProductById_ShouldReturnNull_WhenProductDoesNotExist()
    {
        // Arrange
        var productId = Guid.NewGuid();
        _mockProductRepository.Setup(repo => repo.GetById(It.Is<Guid>(id => id == productId)))
                              .Returns((Product)null);

        // Act
        var result = _productService.GetProductById(productId);

        // Assert
        Assert.IsNull(result);
    }

    [Test]
    public void GetProducts_ShouldReturnProducts_WhenFilterIsApplied()
    {
        // Arrange
        var filter = (Expression<Func<Product, bool>>)(p => p.Id != Guid.Empty);  // Correct usage
        var products = new List<Product>
            {
                new Product { Id = Guid.NewGuid() },
                new Product { Id = Guid.NewGuid() }
            };
        _mockProductRepository.Setup(repo => repo.GetAllAsync(It.IsAny<Expression<Func<Product, bool>>>()))
                              .Returns(products);

        // Act
        var result = _productService.GetProducts(filter);

        // Assert
        Assert.AreEqual(products, result);
    }

    [Test]
    public void Update_ShouldUpdateProduct_WhenProductExists()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var product = new Product { Id = productId };
        _mockProductRepository.Setup(repo => repo.GetById(It.Is<Guid>(id => id == productId)))
                              .Returns(product);

        // Act
        var result = _productService.Update(productId);

        // Assert
        Assert.AreEqual(product, result);
        _mockProductRepository.Verify(repo => repo.Update(It.Is<Product>(p => p.Id == productId)), Times.Once);
    }

    [Test]
    public void Update_ShouldReturnNull_WhenProductDoesNotExist()
    {
        // Arrange
        var productId = Guid.NewGuid();
        _mockProductRepository.Setup(repo => repo.GetById(It.Is<Guid>(id => id == productId)))
                              .Returns((Product)null);

        // Act
        var result = _productService.Update(productId);

        // Assert
        Assert.IsNull(result);
    }
}

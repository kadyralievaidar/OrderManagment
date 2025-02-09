using Moq;
using OrderManagment.Database;
using OrderManagment.Database.UoW;
using OrderManagment.Database.UoW.Repositories;
using OrderManagment.Feautures.Discount.Interfaces;
using OrderManagment.Feautures.Notification.Interfaces;
using OrderManagment.Feautures.Orders.Models;
using OrderManagment.Feautures.Orders.Services;
using OrderManagment.Feautures.Payment.Interfaces;

namespace OrderManagment.Tests.Feautures.Orders;

[TestFixture]
public class OrderServiceTests
{
    private Mock<IOrderRepository> _repositoryMock;
    private Mock<IUnitOfWork> _unitOfWorkMock;
    private Mock<IPayment> _paymentMock;
    private Mock<IDiscountStrategy> _discountMock;
    private Mock<IObserver> _observerMock;
    private OrderService _orderService;
    private Order _testOrder;
    private CancellationToken _cancellationToken;

    [SetUp]
    public void Setup()
    {
        _repositoryMock = new Mock<IOrderRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _paymentMock = new Mock<IPayment>();
        _discountMock = new Mock<IDiscountStrategy>();
        _observerMock = new Mock<IObserver>();

        _orderService = new OrderService(
            _repositoryMock.Object,
            _unitOfWorkMock.Object,
            _paymentMock.Object
        );

        _testOrder = new Order { Id = Guid.NewGuid(), Price = 100m };
        _cancellationToken = new CancellationToken();
    }

    [Test]
    public async Task ProcessOrder_ShouldApplyDiscount_AndSaveOrder()
    {
        // Arrange
        _repositoryMock.Setup(r => r.GetById(_testOrder.Id)).Returns((Order)null);
        _discountMock.Setup(d => d.ApplyDiscount(It.IsAny<decimal>())).Returns(80m);
        _paymentMock.Setup(p => p.Pay(It.IsAny<decimal>())).Returns(1);
        _unitOfWorkMock.Setup(u => u.BeginTransaction()).Returns(Mock.Of<IDbTransaction>());

        // Act
        await _orderService.ProccesOrder(
            _testOrder,
            new List<IObserver> { _observerMock.Object },
            _discountMock.Object,
            _cancellationToken
        );

        // Assert
        _repositoryMock.Verify(r => r.AddAsync(It.Is<Order>(o => o.Price == 80m)), Times.Once);
        _unitOfWorkMock.Verify(u => u.BeginTransaction(), Times.Once);
    }

    [Test]
    public void ProcessOrder_ShouldThrowException_WhenPaymentFails()
    {
        // Arrange
        _repositoryMock.Setup(r => r.GetById(_testOrder.Id)).Returns((Order)null);
        _discountMock.Setup(d => d.ApplyDiscount(It.IsAny<decimal>())).Returns(80m);
        _paymentMock.Setup(p => p.Pay(It.IsAny<decimal>())).Returns(0);
        _unitOfWorkMock.Setup(u => u.BeginTransaction()).Returns(Mock.Of<IDbTransaction>());

        // Act & Assert
        Assert.ThrowsAsync<Exception>(async () =>
            await _orderService.ProccesOrder(
                _testOrder,
                new List<IObserver> { _observerMock.Object },
                _discountMock.Object,
                _cancellationToken
            ));
    }
}

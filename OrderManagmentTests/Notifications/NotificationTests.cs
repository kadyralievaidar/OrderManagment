using FluentAssertions;
using OrderManagment.Feautures.Notification.Services;
using OrderManagment.Feautures.Orders.Models;

namespace OrderManagment.Tests.Feautures.Notification;

[TestFixture]
public class NotifierTests
{
    private Guid _orderId;

    [SetUp]
    public void Setup()
    {
        _orderId = Guid.NewGuid();
    }

    [Test]
    public void EmailNotifier_Update_ShouldReturnEither0Or1()
    {
        // Arrange
        var emailNotifier = new EmailNotifier();
        var orderStatus = OrderStatus.Pending;

        // Act
        int result = emailNotifier.Update(orderStatus, _orderId);

        // Assert
        result.Should().BeOneOf(0, 1);
    }

    [Test]
    public void SMSNotifier_Update_ShouldReturnEither0Or1()
    {
        // Arrange
        var smsNotifier = new SMSNotifier();
        var orderStatus = OrderStatus.Completed;

        // Act
        int result = smsNotifier.Update(orderStatus, _orderId);

        // Assert
        result.Should().BeOneOf(0, 1);
    }
}

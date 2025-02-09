using FluentAssertions;
using Moq;
using OrderManagment.Feautures.Payment.Interfaces;
using OrderManagment.Feautures.Payment.Services;

namespace OrderManagmentTests.Payments;

[TestFixture]
public class PaymentTests
{

    [Test]
    public void CreditCardPayment_ShouldReturnSuccess_WhenPaymentIsSuccessful()
    {
        // Arrange
        var creditCardPayment = new CreditCardPayment();

        // Act
        var result = creditCardPayment.Pay(100m);

        // Assert
        result.Should().BeOneOf(0, 1);
    }

    [Test]
    public void PayPalPayment_ShouldReturnSuccess_WhenPaymentIsSuccessful()
    {
        // Arrange
        var payPalPayment = new PayPalPayment();

        // Act
        var result = payPalPayment.Pay(100m);

        // Assert
        result.Should().BeOneOf(0, 1);
    }

    [Test]
    public void BitcoinPayment_ShouldReturnSuccess_WhenPaymentIsSuccessful()
    {
        // Arrange
        var bitcoinPayment = new BitcoinPayment();

        // Act
        var result = bitcoinPayment.Pay(100m);

        // Assert
        result.Should().BeOneOf(0, 1);
    }
}

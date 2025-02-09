using FluentAssertions;
using OrderManagment.Feautures.Payment.Factory;
using OrderManagment.Feautures.Payment.Services;

namespace OrderManagment.Tests.Feautures.Payment;
[TestFixture]
public class PaymentFactoryTests
{
    [Test]
    public void CreatePayment_ShouldReturnCreditCardPayment_WhenPaymentTypeIsCreditCard()
    {
        // Arrange
        string paymentType = "creditcard";

        // Act
        var payment = PaymentFactory.CreatePayment(paymentType);

        // Assert
        payment.Should().BeOfType<CreditCardPayment>();
    }

    [Test]
    public void CreatePayment_ShouldReturnPayPalPayment_WhenPaymentTypeIsPayPal()
    {
        // Arrange
        string paymentType = "paypal";

        // Act
        var payment = PaymentFactory.CreatePayment(paymentType);

        // Assert
        payment.Should().BeOfType<PayPalPayment>();
    }

    [Test]
    public void CreatePayment_ShouldReturnBitcoinPayment_WhenPaymentTypeIsBitcoin()
    {
        // Arrange
        string paymentType = "bitcoin";

        // Act
        var payment = PaymentFactory.CreatePayment(paymentType);

        // Assert
        payment.Should().BeOfType<BitcoinPayment>();
    }

    [Test]
    public void CreatePayment_ShouldThrowArgumentException_WhenPaymentTypeIsInvalid()
    {
        // Arrange
        string paymentType = "invalid";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => PaymentFactory.CreatePayment(paymentType));
    }
}


using FluentAssertions;
using OrderManagment.Feautures.Discount.Services;

namespace OrderManagment.Tests.Feautures.Discount;

[TestFixture]
public class DiscountStrategyTests
{
    [Test]
    public void MemberDiscount_ApplyDiscount_ShouldReturn20PercentOff()
    {
        // Arrange
        var discountStrategy = new MemberDiscount();
        decimal originalPrice = 100m;

        // Act
        decimal discountedPrice = discountStrategy.ApplyDiscount(originalPrice);

        // Assert
        discountedPrice.Should().Be(80m);
    }

    [Test]
    public void NoDiscountService_ApplyDiscount_ShouldReturnSamePrice()
    {
        // Arrange
        var discountStrategy = new NoDiscountService();
        decimal originalPrice = 100m;

        // Act
        decimal discountedPrice = discountStrategy.ApplyDiscount(originalPrice);

        // Assert
        discountedPrice.Should().Be(originalPrice);
    }

    [Test]
    public void SeasonalDiscount_ApplyDiscount_ShouldReturn50PercentOff()
    {
        // Arrange
        var discountStrategy = new SeasonalDiscount();
        decimal originalPrice = 100m;

        // Act
        decimal discountedPrice = discountStrategy.ApplyDiscount(originalPrice);

        // Assert
        discountedPrice.Should().Be(50m);
    }
}

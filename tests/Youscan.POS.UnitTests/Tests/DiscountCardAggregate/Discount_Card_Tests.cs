using NUnit.Framework;
using Shouldly;
using Youscan.POS.Library.Aggregates.DiscountCardAggregate;

namespace Youscan.POS.UnitTests.Tests.DiscountCardAggregate
{
    [TestFixture]
    public class Discount_Card_Tests
    {
        [Test]
        public void DiscountPercent()
        {
            var discountCard = new DiscountCard(10);
            discountCard.DiscountPercent.ShouldBe(10);
        }

        [Test]
        public void IncreaseAmount()
        {
            var discountCard = new DiscountCard(10);

            var amount = 20m;

            discountCard.IncreaseAmount(amount);
            discountCard.Amount.ShouldBe(amount);

            discountCard.IncreaseAmount(amount);
            discountCard.Amount.ShouldBe(2 * amount);
        }

        [Test]
        public void Apply_WithoutRoundings()
        {
            var discountCard = new DiscountCard(10);
            var priceWithDiscount = discountCard.Apply(100m);

            priceWithDiscount.ShouldBe(90m);
        }

        [Test]
        public void Apply_WithRoundings()
        {
            var discountCard = new DiscountCard(7);
            var priceWithDiscount = discountCard.Apply(91.5m);

            priceWithDiscount.ShouldBe(85.10m);
        }
    }
}

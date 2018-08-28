using Moq;
using NUnit.Framework;
using Shouldly;
using Youscan.POS.Library.Aggregates.DiscountCardAggregate;
using Youscan.POS.Library.Aggregates.ProductAggregate;
using Youscan.POS.Library.Exceptions;

namespace Youscan.POS.UnitTests.Tests.ProductAggregate
{
    [TestFixture]
    public class Product_GetPriceWithDiscount_Tests
    {
        private const decimal PRICE_WITH_DISCOUNT = 45m;

        private IProduct _product;
        private readonly Mock<IDiscountCard> _discountCard;


        public Product_GetPriceWithDiscount_Tests()
        {
            this._discountCard = new Mock<IDiscountCard>();
            this._discountCard.Setup(
                c => c.Apply(It.IsAny<decimal>())
            ).Returns(PRICE_WITH_DISCOUNT);
        }


        [SetUp]
        public void SetUp()
        {
            this._product = new Product("A");
        }


        [Test]
        public void NoPricingRules_Throws()
        {
            var ex = Should.Throw<SinglePriceNotSetException>(() => this._product.GetPriceWithDiscount(1, this._discountCard.Object));
            ex.ProductName.ShouldBe("A");
        }

        [Test]
        public void SinglePriceOnly_NoDiscountCard()
        {
            this._product.SetPricing(10m);

            var price = this._product.GetPriceWithDiscount(5, null);
            price.ShouldBe(50m);
        }

        [Test]
        public void SinglePriceOnly_WithDiscountCard()
        {
            this._product.SetPricing(10m);

            var price = this._product.GetPriceWithDiscount(5, this._discountCard.Object);
            price.ShouldBe(PRICE_WITH_DISCOUNT);
        }

        [Test]
        public void VolumePriceOnly_Throws()
        {
            this._product.SetPricing(2, 10m);
            var ex = Should.Throw<SinglePriceNotSetException>(() => this._product.GetPriceWithDiscount(2, this._discountCard.Object));
            ex.ProductName.ShouldBe("A");
        }

        [Test]
        public void SingleAndVolumePrice_VolumeApplicable_NoDiscountCard()
        {
            this._product.SetPricing(10m);
            this._product.SetPricing(2, 15m);

            var price = this._product.GetPriceWithDiscount(5, null);
            price.ShouldBe(40m);
        }

        [Test]
        public void SingleAndVolumePrice_VolumeApplicable_WithDiscountCard()
        {
            this._product.SetPricing(10m);
            this._product.SetPricing(2, 15m);

            var price = this._product.GetPriceWithDiscount(5, this._discountCard.Object);
            price.ShouldBe(75m);
        }

        [Test]
        public void SingleAndVolumePrice_VolumeNotApplicable_NoDiscountCard()
        {
            this._product.SetPricing(10m);
            this._product.SetPricing(6, 55m);

            var price = this._product.GetPriceWithDiscount(5, null);
            price.ShouldBe(50);
        }

        [Test]
        public void SingleAndVolumePrice_VolumeNotApplicable_WithDiscountCard()
        {
            this._product.SetPricing(10m);
            this._product.SetPricing(6, 55m);

            var price = this._product.GetPriceWithDiscount(5, this._discountCard.Object);
            price.ShouldBe(PRICE_WITH_DISCOUNT);
        }
    }
}

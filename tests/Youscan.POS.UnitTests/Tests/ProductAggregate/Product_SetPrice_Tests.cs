using System.Linq;
using NUnit.Framework;
using Shouldly;
using Youscan.POS.Library.Aggregates.ProductAggregate;
using Youscan.POS.Library.Aggregates.ProductAggregate.PricingRules;
using Youscan.POS.Library.Exceptions;

namespace Youscan.POS.UnitTests.Tests.ProductAggregate
{
    [TestFixture]
    public class Product_SetPrice_Tests
    {
        private IProduct _product;


        [SetUp]
        public void SetUp()
        {
            this._product = new Product("A");
        }


        [Test]
        public void SetPrice_SinglePrice_Valid()
        {
            this._product.SetPricing(10m);

            this._product.PricingRules.Count.ShouldBe(1);
            var pricingRule = this._product.PricingRules.Single();
            var singlePricingRule = pricingRule.ShouldBeOfType<SinglePricingRule>();
            singlePricingRule.Price.ShouldBe(10m);
        }

        [TestCase(0)]
        [TestCase(-10)]
        public void SetPrice_SinglePrice_InvalidPrice(decimal price)
        {
            var ex = Should.Throw<InvalidPriceException>(() => this._product.SetPricing(price));
            ex.Price.ShouldBe(price);
        }

        [Test]
        public void SetPrice_VolumePrice_Valid()
        {
            this._product.SetPricing(2, 10m);

            this._product.PricingRules.Count.ShouldBe(1);
            var pricingRule = this._product.PricingRules.Single();
            var volumePricingRule = pricingRule.ShouldBeOfType<VolumePricingRule>();
            volumePricingRule.Price.ShouldBe(10m);
            volumePricingRule.Count.ShouldBe(2);
        }

        [TestCase(0)]
        [TestCase(-10)]
        public void SetPrice_VolumePrice_InvalidPrice(decimal price)
        {
            var ex = Should.Throw<InvalidPriceException>(() => this._product.SetPricing(2, price));
            ex.Price.ShouldBe(price);
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void SetPrice_VolumePrice_InvalidCount(int count)
        {
            var ex = Should.Throw<InvalidProductCountException>(() => this._product.SetPricing(count, 10m));
            ex.Count.ShouldBe(count);
        }

        [Test]
        public void SetPrice_VolumePrice_SinglePrice()
        {
            this._product.SetPricing(1, 10m);

            this._product.PricingRules.Count.ShouldBe(1);
            var pricingRule = this._product.PricingRules.Single();
            var singlePricingRule = pricingRule.ShouldBeOfType<SinglePricingRule>();
            singlePricingRule.Price.ShouldBe(10m);
        }
    }
}
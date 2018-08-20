using NUnit.Framework;
using Shouldly;
using Youscan.POS.Library.Aggregates.ProductAggregate;
using Youscan.POS.Library.Exceptions;

namespace Youscan.POS.UnitTests.Tests.ProductAggregate
{
    [TestFixture]
    public class Product_GetPrice_Tests
    {
        private IProduct _product;


        [SetUp]
        public void SetUp()
        {
            this._product = new Product("A");
        }


        [Test]
        public void GetPrice_NoPricingRules_Throws()
        {
            var ex = Should.Throw<SinglePriceNotSetException>(() => this._product.GetPrice(1));
            ex.ProductName.ShouldBe("A");
        }

        [Test]
        public void GetPrice_SinglePriceOnly()
        {
            this._product.SetPricing(10m);

            var price = this._product.GetPrice(5);
            price.ShouldBe(50m);
        }

        [Test]
        public void GetPrice_VolumePriceOnly_Throws()
        {
            this._product.SetPricing(2, 10m);
            var ex = Should.Throw<SinglePriceNotSetException>(() => this._product.GetPrice(2));
            ex.ProductName.ShouldBe("A");
        }

        [Test]
        public void GetPrice_SingleAndVolumePrice_VolumeApplicable()
        {
            this._product.SetPricing(10m);
            this._product.SetPricing(2, 15m);

            var price = this._product.GetPrice(5);
            price.ShouldBe(40);
        }

        [Test]
        public void GetPrice_SingleAndVolumePrice_VolumeNotApplicable()
        {
            this._product.SetPricing(10m);
            this._product.SetPricing(6, 55m);

            var price = this._product.GetPrice(5);
            price.ShouldBe(50);
        }
    }
}

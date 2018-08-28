using Moq;
using NUnit.Framework;
using Shouldly;
using Youscan.POS.Library.Aggregates.CartAggregate;
using Youscan.POS.Library.Aggregates.DiscountCardAggregate;
using Youscan.POS.Library.Aggregates.ProductAggregate;

namespace Youscan.POS.UnitTests.Tests.CartAggregate
{
    [TestFixture]
    public class Cart_Item_Tests
    {
        private const string PRODUCT_NAME = "A";
        private const decimal PRODUCT_PRICE = 15m;

        private readonly Mock<IProduct> _mockProduct;


        public Cart_Item_Tests()
        {
            this._mockProduct = new Mock<IProduct>();
            this._mockProduct.SetupGet(
                p => p.Name
            ).Returns(PRODUCT_NAME);
            this._mockProduct.Setup(
                p => p.GetPriceWithDiscount(It.IsAny<int>(), It.IsAny<IDiscountCard>())
            ).Returns(PRODUCT_PRICE);
            this._mockProduct.Setup(
                p => p.GetFullPrice(It.IsAny<int>())
            ).Returns(PRODUCT_PRICE);
        }


        [Test]
        public void Creation_NoCount()
        {
            var cartItem = new CartItem(this._mockProduct.Object);

            cartItem.Product.Name.ShouldBe(PRODUCT_NAME);
            cartItem.Count.ShouldBe(1);
            cartItem.FullPrice.ShouldBe(PRODUCT_PRICE);
            cartItem.GetPriceWithDiscount(null).ShouldBe(PRODUCT_PRICE);
        }

        [Test]
        public void Creation_WithCount()
        {
            var cartItem = new CartItem(this._mockProduct.Object, 15);

            cartItem.Product.Name.ShouldBe(PRODUCT_NAME);
            cartItem.Count.ShouldBe(15);
        }

        [Test]
        public void Increase_Decrease()
        {
            var cartItem = new CartItem(this._mockProduct.Object);
            cartItem.Count.ShouldBe(1);

            cartItem.IncreaseCount();
            cartItem.Count.ShouldBe(2);

            cartItem.DecreaseCount();
            cartItem.Count.ShouldBe(1);
        }
    }
}
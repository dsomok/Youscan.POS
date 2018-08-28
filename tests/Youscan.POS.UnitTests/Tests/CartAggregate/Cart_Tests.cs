using System;
using System.Linq;
using Moq;
using NUnit.Framework;
using Shouldly;
using Youscan.POS.Library.Aggregates.CartAggregate;
using Youscan.POS.Library.Aggregates.DiscountCardAggregate;
using Youscan.POS.Library.Aggregates.ProductAggregate;

namespace Youscan.POS.UnitTests.Tests.CartAggregate
{
    [TestFixture]
    public class Cart_Tests
    {
        private const string PRODUCT_NAME = "A";
        private const decimal PRODUCT_PRICE = 15m;
        private const decimal PRODUCT_PRICE_WITH_DISCOUNT = 10m;

        private ICart _cart;
        private readonly IProduct _existingProduct;


        public Cart_Tests()
        {
            var mockProduct = new Mock<IProduct>();
            mockProduct.SetupGet(
                p => p.Name
            ).Returns(PRODUCT_NAME);
            mockProduct.Setup(
                p => p.GetFullPrice(It.IsAny<int>())
            ).Returns((int count) => count * PRODUCT_PRICE);
            mockProduct.Setup(
                p => p.GetPriceWithDiscount(It.IsAny<int>(), It.Is<IDiscountCard>(card => card == null))
            ).Returns((int count, IDiscountCard card) => count * PRODUCT_PRICE);
            mockProduct.Setup(
                p => p.GetPriceWithDiscount(It.IsAny<int>(), It.Is<IDiscountCard>(card => card != null))
            ).Returns((int count, IDiscountCard card) => count * PRODUCT_PRICE_WITH_DISCOUNT);

            this._existingProduct = mockProduct.Object;
        }

        [SetUp]
        public void SetUp()
        {
            this._cart = new Cart
            {
                this._existingProduct
            };
        }


        [Test]
        public void Add_ExistingProduct()
        {
            this._cart.Add(this._existingProduct);

            this._cart.Count().ShouldBe(1);

            var cartItem = this._cart.First();
            cartItem.Product.Id.ShouldBe(this._existingProduct.Id);
            cartItem.Product.Name.ShouldBe(this._existingProduct.Name);
            cartItem.Count.ShouldBe(2);

            this._cart.PriceWithDiscount.ShouldBe(2 * PRODUCT_PRICE);
            this._cart.FullPrice.ShouldBe(2 * PRODUCT_PRICE);
        }

        [Test]
        public void Add_NewProduct()
        {
            var mockProduct = new Mock<IProduct>();
            mockProduct.SetupGet(
                p => p.Id
            ).Returns(Guid.NewGuid());
            mockProduct.SetupGet(
                p => p.Name
            ).Returns("B");
            mockProduct.Setup(
                p => p.GetFullPrice(It.IsAny<int>())
            ).Returns(25m);
            mockProduct.Setup(
                p => p.GetPriceWithDiscount(It.IsAny<int>(), It.IsAny<IDiscountCard>())
            ).Returns(20m);

            this._cart.Add(mockProduct.Object);

            this._cart.Count().ShouldBe(2);

            this._cart.ShouldContain(item =>
                item.Product.Id == mockProduct.Object.Id &&
                item.Count == 1
            );

            this._cart.PriceWithDiscount.ShouldBe(PRODUCT_PRICE + 20m);
            this._cart.FullPrice.ShouldBe(PRODUCT_PRICE + 25m);
        }


        [Test]
        public void ApplyDiscountCard()
        {
            this._cart.Add(this._existingProduct);

            var discountCard = new DiscountCard(10);
            this._cart.ApplyCard(discountCard);

            this._cart.PriceWithDiscount.ShouldBe(2 * PRODUCT_PRICE_WITH_DISCOUNT);
            this._cart.FullPrice.ShouldBe(2 * PRODUCT_PRICE);
        }


        [Test]
        public void Clear()
        {
            this._cart.Clear();

            this._cart.Count().ShouldBe(0);
            this._cart.PriceWithDiscount.ShouldBe(0m);
            this._cart.FullPrice.ShouldBe(0m);
        }
    }
}

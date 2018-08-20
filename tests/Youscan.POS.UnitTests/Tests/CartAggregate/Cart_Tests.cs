using System;
using System.Linq;
using Moq;
using NUnit.Framework;
using Shouldly;
using Youscan.POS.Library.Aggregates.CartAggregate;
using Youscan.POS.Library.Aggregates.ProductAggregate;

namespace Youscan.POS.UnitTests.Tests.CartAggregate
{
    [TestFixture]
    public class Cart_Tests
    {
        private const string PRODUCT_NAME = "A";
        private const decimal PRODUCT_PRICE = 15m;

        private ICart _cart;
        private readonly IProduct _existingProduct;


        public Cart_Tests()
        {
            var mockProduct = new Mock<IProduct>();
            mockProduct.SetupGet(
                p => p.Name
            ).Returns(PRODUCT_NAME);
            mockProduct.Setup(
                p => p.GetPrice(It.IsAny<int>())
            ).Returns(PRODUCT_PRICE);

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
            this._cart.TotalPrice.ShouldBe(PRODUCT_PRICE);
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
                p => p.GetPrice(It.IsAny<int>())
            ).Returns(20m);

            this._cart.Add(mockProduct.Object);

            this._cart.Count().ShouldBe(2);

            this._cart.ShouldContain(item =>
                item.Product.Id == mockProduct.Object.Id &&
                item.Count == 1
            );

            this._cart.TotalPrice.ShouldBe(PRODUCT_PRICE + 20m);
        }


        [Test]
        public void Clear()
        {
            this._cart.Clear();

            this._cart.Count().ShouldBe(0);
            this._cart.TotalPrice.ShouldBe(0m);
        }
    }
}

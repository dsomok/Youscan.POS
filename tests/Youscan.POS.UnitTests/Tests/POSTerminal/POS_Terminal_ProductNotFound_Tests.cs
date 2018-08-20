using Moq;
using NUnit.Framework;
using Shouldly;
using Youscan.POS.Library;
using Youscan.POS.Library.Aggregates.CartAggregate;
using Youscan.POS.Library.Aggregates.ProductAggregate;
using Youscan.POS.Library.Exceptions;
using Youscan.POS.Library.Repositories;

namespace Youscan.POS.UnitTests.Tests.POSTerminal
{
    [TestFixture]
    public class POS_Terminal_ProductNotFound_Tests
    {
        private const string PRODUCT_NAME = "A";

        private Mock<IProduct> _mockProduct;
        private Mock<IProductRepository> _mockProductRepository;


        [SetUp]
        public void SetUp()
        {
            this._mockProduct = new Mock<IProduct>();
            this._mockProduct.SetupGet(
                p => p.Name
            ).Returns(PRODUCT_NAME);
            this._mockProduct.Setup(
                p => p.SetPricing(It.IsAny<decimal>())
            ).Verifiable();
            this._mockProduct.Setup(
                p => p.SetPricing(It.IsAny<int>(), It.IsAny<decimal>())
            ).Verifiable();

            this._mockProductRepository = new Mock<IProductRepository>();
            this._mockProductRepository.Setup(
                r => r.Get(It.IsAny<string>())
            ).Returns<Product>(null);
            this._mockProductRepository.Setup(
                r => r.TryGet(It.IsAny<string>(), out It.Ref<IProduct>.IsAny)
            ).Returns(false);

            this._mockProductRepository.Setup(
                r => r.Create(It.IsAny<string>())
            ).Returns(this._mockProduct.Object).Verifiable();
        }


        [Test]
        public void SetPricing_SinglePrice()
        {
            var productPrice = 10m;
            var terminal = new PointOfSaleTerminal(new Cart(), this._mockProductRepository.Object);
            terminal.SetPricing(PRODUCT_NAME, productPrice);

            this._mockProductRepository.Verify(p => p.Create(It.Is<string>(name => name == PRODUCT_NAME)), Times.Once);
            this._mockProduct.Verify(p => p.SetPricing(It.Is<decimal>(price => price == productPrice)), Times.Once);
        }

        [Test]
        public void SetPricing_VolumePrice()
        {
            var productPrice = 10m;
            var productCount = 3;
            var terminal = new PointOfSaleTerminal(new Cart(), this._mockProductRepository.Object);
            terminal.SetPricing(PRODUCT_NAME, productCount, productPrice);

            this._mockProductRepository.Verify(p => p.Create(It.Is<string>(name => name == PRODUCT_NAME)), Times.Once);
            this._mockProduct.Verify(p => p.SetPricing(
                It.Is<int>(count => count == productCount), 
                It.Is<decimal>(price => price == productPrice)
            ), Times.Once);
        }

        [Test]
        public void Scan()
        {
            var terminal = new PointOfSaleTerminal(new Cart(), this._mockProductRepository.Object);

            var ex = Should.Throw<ProductNotFoundException>(() => terminal.Scan("B"));
            ex.ProductName.ShouldBe("B");
        }
    }
}
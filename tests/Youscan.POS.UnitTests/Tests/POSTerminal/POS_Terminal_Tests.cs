using Moq;
using NUnit.Framework;
using Shouldly;
using Youscan.POS.Library;
using Youscan.POS.Library.Aggregates.CartAggregate;
using Youscan.POS.Library.Repositories;

namespace Youscan.POS.UnitTests.Tests.POSTerminal
{
    [TestFixture]
    public class POS_Terminal_Tests
    {
        private const decimal TOTAL_PRICE = 100m;

        private Mock<ICart> _mockCart;
        private Mock<IProductRepository> _mockProductRepository;


        [SetUp]
        public void SetUp()
        {
            this._mockCart = new Mock<ICart>();
            this._mockCart.SetupGet(
                c => c.PriceWithDiscount
            ).Returns(TOTAL_PRICE);

            this._mockCart.Setup(
                c => c.FinishSale()
            ).Returns(TOTAL_PRICE);

            this._mockCart.Setup(
                c => c.Clear()
            ).Verifiable();

            this._mockProductRepository = new Mock<IProductRepository>();
        }


        [Test]
        public void CalculateTotal()
        {
            var terminal = new PointOfSaleTerminal(this._mockCart.Object, this._mockProductRepository.Object);
            var result = terminal.CalculateTotal();

            result.ShouldBe(100d);
        }

        [Test]
        public void Reset()
        {
            var terminal = new PointOfSaleTerminal(this._mockCart.Object, this._mockProductRepository.Object);
            terminal.Reset();

            this._mockCart.Verify(c => c.Clear(), Times.Once);
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Shouldly;
using Youscan.POS.Library.Aggregates.ProductAggregate;
using Youscan.POS.Library.Repositories;

namespace Youscan.POS.UnitTests.Tests.ProductRepository
{
    [TestFixture]
    public class ProductRepository_Tests
    {
        private IProductRepository _productRepository;

        [SetUp]
        public void SetUp()
        {
            var testProducts = new List<IProduct>
            {
                new Product("A"),
                new Product("B"),
                new Product("C")
            };

            this._productRepository = new Library.Repositories.ProductRepository(testProducts);
        }

        [Test]
        public void Create_New()
        {
            var newProduct = this._productRepository.Create("D");

            newProduct.ShouldNotBeNull();
            newProduct.Name.ShouldBe("D");
            this._productRepository.Count().ShouldBe(4);
        }

        [Test]
        public void Create_Existing()
        {
            var newProduct = this._productRepository.Create("C");

            newProduct.ShouldNotBeNull();
            newProduct.Name.ShouldBe("C");
            this._productRepository.Count().ShouldBe(3);
        }

        [Test]
        public void Get_Existing()
        {
            var product = this._productRepository.Get("A");
            product.ShouldNotBeNull();
            product.Name.ShouldBe("A");
        }

        [Test]
        public void Get_NonExisting()
        {
            var product = this._productRepository.Get("F");
            product.ShouldBeNull();
        }

        [Test]
        public void TryGet_Existing()
        {
            var result = this._productRepository.TryGet("A", out var product);

            result.ShouldBeTrue();
            product.ShouldNotBeNull();
            product.Name.ShouldBe("A");
        }

        [Test]
        public void TryGet_NonExisting()
        {
            var result = this._productRepository.TryGet("F", out var product);

            result.ShouldBeFalse();
            product.ShouldBeNull();
        }
    }
}

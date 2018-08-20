using Youscan.POS.Library.Aggregates.CartAggregate;
using Youscan.POS.Library.Exceptions;
using Youscan.POS.Library.Repositories;

namespace Youscan.POS.Library
{
    public class PointOfSaleTerminal : IPointOfSaleTerminal
    {
        private readonly ICart _cart;
        private readonly IProductRepository _productRepository;


        public PointOfSaleTerminal()
            : this(new Cart(), new ProductRepository())
        {

        }

        internal PointOfSaleTerminal(ICart cart, IProductRepository productRepository)
        {
            this._cart = cart;
            this._productRepository = productRepository;
        }


        public void SetPricing(string productName, decimal singlePrice)
        {
            if (!this._productRepository.TryGet(productName, out var product))
            {
                product = this._productRepository.Create(productName);
            }

            product.SetPricing(singlePrice);
        }

        public void SetPricing(string productName, int volumeCount, decimal volumePrice)
        {
            if (!this._productRepository.TryGet(productName, out var product))
            {
                product = this._productRepository.Create(productName);
            }

            product.SetPricing(volumeCount, volumePrice);
        }


        public void Scan(string productName)
        {
            if (!this._productRepository.TryGet(productName, out var product))
            {
                throw new ProductNotFoundException(productName);
            }

            this._cart.Add(product);
        }


        public double CalculateTotal()
        {
            var totalPrice = this._cart.TotalPrice;
            return decimal.ToDouble(totalPrice);
        }


        public void Reset()
        {
            this._cart.Clear();
        }
    }
}

using Youscan.POS.Library.Aggregates.DiscountCardAggregate;
using Youscan.POS.Library.Aggregates.ProductAggregate;

namespace Youscan.POS.Library.Aggregates.CartAggregate
{
    internal class CartItem
    {
        public CartItem(IProduct product)
            : this(product, 1)
        {
        }

        public CartItem(IProduct product, int count)
        {
            Product = product;
            Count = count;
        }


        public IProduct Product { get; }

        public int Count { get; private set; }


        public decimal FullPrice
            => this.Product.GetFullPrice(this.Count);

        public decimal GetPriceWithDiscount(IDiscountCard discountCard)
            => this.Product.GetPriceWithDiscount(this.Count, discountCard);


        public void IncreaseCount()
        {
            this.Count++;
        }

        public void DecreaseCount()
        {
            if (this.Count > 0)
            {
                this.Count--;
            }
        }
    }
}
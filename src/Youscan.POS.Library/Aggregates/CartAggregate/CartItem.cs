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


        public decimal Price => this.Product.GetPrice(this.Count);


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
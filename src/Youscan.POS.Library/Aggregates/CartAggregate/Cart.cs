using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Youscan.POS.Library.Aggregates.DiscountCardAggregate;
using Youscan.POS.Library.Aggregates.ProductAggregate;

namespace Youscan.POS.Library.Aggregates.CartAggregate
{
    internal class Cart : ICart
    {
        private readonly IDictionary<Guid, CartItem> _cartItems;


        public Cart()
        {
            this._cartItems = new Dictionary<Guid, CartItem>();
            this.FullPrice = 0m;
            this.PriceWithDiscount = 0m;
        }


        public IDiscountCard DiscountCard { get; private set; }

        public decimal FullPrice { get; private set; }
        public decimal PriceWithDiscount { get; private set; }


        public void Add(IProduct product)
        {
            if (this._cartItems.TryGetValue(product.Id, out var cartItem))
            {
                var initialPrice = cartItem.GetPriceWithDiscount(this.DiscountCard);
                this.PriceWithDiscount -= initialPrice;

                cartItem.IncreaseCount();
            }
            else
            {
                cartItem = new CartItem(product);
                this._cartItems[product.Id] = cartItem;
            }
            
            this.PriceWithDiscount += cartItem.GetPriceWithDiscount(this.DiscountCard);
            this.FullPrice += product.GetFullPrice(1);
        }

        public void ApplyCard(IDiscountCard discountCard)
        {
            this.DiscountCard = discountCard;
            this.PriceWithDiscount = this._cartItems.Values.Sum(ci => ci.GetPriceWithDiscount(discountCard));
        }

        public decimal FinishSale()
        {
            var totalPrice = this.PriceWithDiscount;

            this.DiscountCard?.IncreaseAmount(this.FullPrice);
            this.Clear();

            return totalPrice;
        }

        public void Clear()
        {
            this._cartItems.Clear();
            this.PriceWithDiscount = 0m;
            this.FullPrice = 0m;
        }


        #region IEnumerable

        public IEnumerator<CartItem> GetEnumerator()
        {
            return this._cartItems.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this._cartItems.Values.GetEnumerator();
        } 

        #endregion
    }
}

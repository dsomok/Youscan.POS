using System;
using System.Collections;
using System.Collections.Generic;
using Youscan.POS.Library.Aggregates.ProductAggregate;

namespace Youscan.POS.Library.Aggregates.CartAggregate
{
    internal class Cart : ICart
    {
        private readonly IDictionary<Guid, CartItem> _cartItems;


        public Cart()
        {
            this._cartItems = new Dictionary<Guid, CartItem>();
            this.TotalPrice = 0m;
        }


        public decimal TotalPrice { get; private set; }


        public void Add(IProduct product)
        {
            if (this._cartItems.TryGetValue(product.Id, out var cartItem))
            {
                var initialPrice = cartItem.Price;

                cartItem.IncreaseCount();

                this.TotalPrice += cartItem.Price - initialPrice;
            }
            else
            {
                cartItem = new CartItem(product);
                this._cartItems[product.Id] = cartItem;

                this.TotalPrice += cartItem.Price;
            }
        }

        public void Clear()
        {
            this._cartItems.Clear();
            this.TotalPrice = 0m;
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

using System.Collections.Generic;
using Youscan.POS.Library.Aggregates.ProductAggregate;

namespace Youscan.POS.Library.Aggregates.CartAggregate
{
    internal interface ICart : IEnumerable<CartItem>
    {
        decimal TotalPrice { get; }
        void Add(IProduct product);
        void Clear();
    }
}
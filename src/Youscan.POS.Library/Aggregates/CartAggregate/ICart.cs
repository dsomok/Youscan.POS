using System.Collections.Generic;
using Youscan.POS.Library.Aggregates.DiscountCardAggregate;
using Youscan.POS.Library.Aggregates.ProductAggregate;

namespace Youscan.POS.Library.Aggregates.CartAggregate
{
    internal interface ICart : IEnumerable<CartItem>
    {
        IDiscountCard DiscountCard { get; }

        decimal FullPrice { get; }
        decimal PriceWithDiscount { get; }

        void Add(IProduct product);
        void ApplyCard(IDiscountCard discountCard);

        decimal FinishSale();
        void Clear();
    }
}
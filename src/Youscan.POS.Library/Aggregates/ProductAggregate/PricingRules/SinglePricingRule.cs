using System;
using Youscan.POS.Library.Entities.PricingRules;

namespace Youscan.POS.Library.Aggregates.ProductAggregate.PricingRules
{
    internal class SinglePricingRule : IPricingRule
    {
        public SinglePricingRule(Guid productId, decimal price)
        {
            ProductId = productId;
            Price = price;
        }

        public Guid ProductId { get; }
        public decimal Price { get; }
    }
}

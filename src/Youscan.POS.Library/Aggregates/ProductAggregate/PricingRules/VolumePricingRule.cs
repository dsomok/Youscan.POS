using System;
using Youscan.POS.Library.Entities.PricingRules;

namespace Youscan.POS.Library.Aggregates.ProductAggregate.PricingRules
{
    internal class VolumePricingRule : IPricingRule
    {
        public VolumePricingRule(Guid productId, int count, decimal price)
        {
            ProductId = productId;
            Count = count;
            Price = price;
        }

        public Guid ProductId { get; }
        public int Count { get; }
        public decimal Price { get; }
    }
}
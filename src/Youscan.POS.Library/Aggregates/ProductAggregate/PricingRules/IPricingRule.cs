using System;

namespace Youscan.POS.Library.Entities.PricingRules
{
    internal interface IPricingRule
    {
        Guid ProductId { get; }
        decimal Price { get; }
    }
}
﻿using System;
using System.Collections.Generic;
using Youscan.POS.Library.Aggregates.DiscountCardAggregate;
using Youscan.POS.Library.Entities.PricingRules;

namespace Youscan.POS.Library.Aggregates.ProductAggregate
{
    internal interface IProduct
    {
        Guid Id { get; }
        string Name { get; }

        IReadOnlyCollection<IPricingRule> PricingRules { get; }

        void SetPricing(decimal price);
        void SetPricing(int count, decimal price);

        decimal GetFullPrice(int count);
        decimal GetPriceWithDiscount(int count, IDiscountCard discountCard);
    }
}
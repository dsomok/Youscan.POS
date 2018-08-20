using System;
using System.Collections.Generic;
using Youscan.POS.Library.Aggregates.ProductAggregate.PricingRules;
using Youscan.POS.Library.Entities.PricingRules;
using Youscan.POS.Library.Exceptions;

namespace Youscan.POS.Library.Aggregates.ProductAggregate
{
    internal class Product : IProduct
    {
        private SinglePricingRule _singlePricingRule = null;
        private VolumePricingRule _volumePricingRule = null;


        public Product(string name)
            : this(Guid.NewGuid(), name)
        {
        }

        public Product(Guid id, string name)
        {
            Id = id;
            Name = name;
        }


        public Guid Id { get; }
        public string Name { get; }

        public IReadOnlyCollection<IPricingRule> PricingRules
        {
            get
            {
                var rules = new List<IPricingRule>();
                if (this._volumePricingRule != null)
                {
                    rules.Add(this._volumePricingRule);
                }

                if (this._singlePricingRule != null)
                {
                    rules.Add(this._singlePricingRule);
                }

                return rules;
            }
        }


        public void SetPricing(decimal price)
        {
            if (price <= 0m)
            {
                throw new InvalidPriceException(price);
            }

            var pricingRule = new SinglePricingRule(this.Id, price);
            this._singlePricingRule = pricingRule;
        }

        public void SetPricing(int count, decimal price)
        {
            if (count <= 0)
            {
                throw new InvalidProductCountException(count);
            }

            if (count == 1)
            {
                this.SetPricing(price);
                return;
            }

            if (price <= 0m)
            {
                throw new InvalidPriceException(price);
            }


            var pricingRule = new VolumePricingRule(this.Id, count, price);
            this._volumePricingRule = pricingRule;
        }


        public decimal GetPrice(int count)
        {
            if (this._singlePricingRule == null)
            {
                throw new SinglePriceNotSetException(this.Name);
            }

            if (this._volumePricingRule == null)
            {
                return this._singlePricingRule.Price * count;
            }


            int volumesCount = count / this._volumePricingRule.Count;
            int singlesCount = count % this._volumePricingRule.Count;

            var totalPrice = volumesCount * this._volumePricingRule.Price +
                             singlesCount * this._singlePricingRule.Price;

            return totalPrice;
        }
    }
}
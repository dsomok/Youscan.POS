using System;

namespace Youscan.POS.Library.Aggregates.DiscountCardAggregate
{
    public class DiscountCard : IDiscountCard
    {
        public DiscountCard(int discountPercent)
        {
            DiscountPercent = discountPercent;
            Amount = 0m;
        }


        public int DiscountPercent { get; }

        public decimal Amount { get; private set; }


        public decimal Apply(decimal total)
        {
            var discount = Math.Floor(total * this.DiscountPercent) / 100;
            return total - discount;
        }

        public void IncreaseAmount(decimal amount)
        {
            this.Amount += amount;
        }
    }
}

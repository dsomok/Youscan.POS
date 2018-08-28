namespace Youscan.POS.Library.Aggregates.DiscountCardAggregate
{
    public interface IDiscountCard
    {
        int DiscountPercent { get; }
        decimal Amount { get; }

        decimal Apply(decimal total);
        void IncreaseAmount(decimal amount);
    }
}
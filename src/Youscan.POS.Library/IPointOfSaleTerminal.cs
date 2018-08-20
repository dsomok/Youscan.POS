namespace Youscan.POS.Library
{
    public interface IPointOfSaleTerminal
    {
        void SetPricing(string productName, decimal singlePrice);
        void SetPricing(string productName, int volumeCount, decimal volumePrice);

        void Scan(string productName);

        double CalculateTotal();
        void Reset();
    }
}
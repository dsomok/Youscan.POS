namespace Youscan.POS.IntegrationTests.Models
{
    public class ProductPriceModel
    {
        public string ProductName { get; set; }
        public decimal SinglePrice { get; set; }
        public int? VolumeCount { get; set; }
        public decimal? VolumePrice { get; set; }
    }
}

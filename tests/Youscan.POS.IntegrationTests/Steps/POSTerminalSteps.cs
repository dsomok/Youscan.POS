using System;
using System.Linq;
using Shouldly;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Youscan.POS.IntegrationTests.Models;
using Youscan.POS.Library;
using Youscan.POS.Library.Aggregates.DiscountCardAggregate;
using Youscan.POS.Library.Exceptions;

namespace Youscan.POS.IntegrationTests.Steps
{
    [Binding]
    public class POSTerminalSteps
    {
        private IDiscountCard _discountCard = null;
        private readonly IPointOfSaleTerminal _terminal = new PointOfSaleTerminal();

        [Given("I have products:")]
        public void IHaveProducts(Table table)
        {
            var products = table.CreateSet<ProductPriceModel>();

            foreach (var product in products)
            {
                this._terminal.SetPricing(product.ProductName, product.SinglePrice);
                if (product.VolumeCount != null && product.VolumePrice != null)
                {
                    this._terminal.SetPricing(
                        productName: product.ProductName,
                        volumeCount: product.VolumeCount.Value,
                        volumePrice: product.VolumePrice.Value
                    );
                }
            }
        }

        [When("I scan products (.*)")]
        public void IScanProducts(string scannedProducts)
        {
            var productNames = scannedProducts.Select(c => c.ToString());
            foreach (var name in productNames)
            {
                this._terminal.Scan(name);
            }
        }

        [When("I try to scan products (.*)")]
        public void ITryToScanProducts(string scannedProducts)
        {
            try
            {
                var productNames = scannedProducts.Select(c => c.ToString());
                foreach (var name in productNames)
                {
                    this._terminal.Scan(name);
                }
            }
            catch (Exception ex)
            {
                ScenarioContext.Current["Exception"] = ex;
            }
        }

        [When("I use discount card with (.*)% of discount")]
        public void IUseDiscountCard(int percent)
        {
            this._discountCard = new DiscountCard(percent);
            this._terminal.ApplyDiscountCard(this._discountCard);
        }

        [Then("Total price will be (.*)")]
        public void TotalPriceWillBe(double totalPrice)
        {
            var result = this._terminal.CalculateTotal();
            result.ShouldBe(totalPrice);
        }

        [Then("I will get exception that product (.*) doesn't exist")]
        public void ExceptionThatProductDoesntExist(string notFound)
        {
            ScenarioContext.Current.Keys.ShouldContain("Exception");
            var ex = ScenarioContext.Current["Exception"];
            ex.ShouldNotBeNull();
            var notFoundException = ex.ShouldBeOfType<ProductNotFoundException>();
            notFoundException.ProductName.ShouldBe(notFound);
        }

        [Then("There will be (.*) on discount card")]
        public void AmountOnDiscountCard(decimal amount)
        {
            this._discountCard.Amount.ShouldBe(amount);
        }
    }
}

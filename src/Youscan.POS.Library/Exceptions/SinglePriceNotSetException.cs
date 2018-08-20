using System;

namespace Youscan.POS.Library.Exceptions
{
    public class SinglePriceNotSetException : Exception
    {
        public SinglePriceNotSetException(string productName)
        {
            ProductName = productName;
        }


        public string ProductName { get; }


        public override string Message => $"Price for single item should be set for product {this.ProductName}";
    }
}
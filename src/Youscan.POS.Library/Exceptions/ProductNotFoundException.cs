using System;

namespace Youscan.POS.Library.Exceptions
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException(string productName)
        {
            ProductName = productName;
        }


        public string ProductName { get; }


        public override string Message => $"Product \"{this.ProductName}\" not found";
    }
}

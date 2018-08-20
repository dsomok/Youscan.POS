using System;

namespace Youscan.POS.Library.Exceptions
{
    public class InvalidPriceException : Exception
    {
        public InvalidPriceException(decimal price)
        {
            Price = price;
        }

        public decimal Price { get; }

        public override string Message => $"Price \"{this.Price}\" is invalid";
    }
}

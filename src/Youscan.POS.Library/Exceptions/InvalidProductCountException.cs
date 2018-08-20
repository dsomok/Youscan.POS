using System;

namespace Youscan.POS.Library.Exceptions
{
    public class InvalidProductCountException : Exception
    {
        public InvalidProductCountException(int count)
        {
            Count = count;
        }

        public int Count { get; }

        public override string Message => $"Product count cannot be equal tp \"{this.Count}\"";
    }
}
using System;

namespace Common.Exceptions
{
    public class CoinNotFoundForReturnException : Exception
    {
        public CoinNotFoundForReturnException(string message)
            : base(message)
        {
        }
    }
}

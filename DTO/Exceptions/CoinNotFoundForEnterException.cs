using System;

namespace Common.Exceptions
{
    public class CoinNotFoundForEnterException : Exception
    {
        public CoinNotFoundForEnterException(string message)
            : base(message)
        {
        }
    }
}

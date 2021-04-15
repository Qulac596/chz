using chz.Common;
using System;

namespace chz.WindowsServices.Cryptography
{
    /*
     * Исключение при работе с криптографией
     */
    public class CryptographyException : CZException
    {
        public CryptographyException(string message) : base(message)
        {

        }

        public CryptographyException(string message, Exception innerException) : base(message, innerException) { }
    }
}

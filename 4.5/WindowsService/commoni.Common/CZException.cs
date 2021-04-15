using System;

namespace chz.Common
{
    /*
     * Базовый класс для всех типов исключений
     */
    public abstract class CZException : Exception
    {
       
        protected CZException(string message) : base(message)
        {

        }
        protected CZException(string message, Exception innerException) : base(message, innerException) { }
    }
}

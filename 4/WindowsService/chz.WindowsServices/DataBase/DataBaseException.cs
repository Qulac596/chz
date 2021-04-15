using chz.Common;
using System;

namespace chz.WindowsServices.DataBase
{
    /*
     * Исключение бызы данных
     */
    public class DataBaseException : CZException
    {
        public DataBaseException(string message, Exception innerException) : base(message, innerException) { }
    }
}

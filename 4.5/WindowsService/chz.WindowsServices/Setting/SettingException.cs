using chz.Common;
using System;

namespace chz.WindowsServices.Setting
{
    public class SettingException : CZException
    {
        public SettingException(string message) : base(message) { }

        public SettingException(string message, Exception innerException) : base(message, innerException) { }
    }
}

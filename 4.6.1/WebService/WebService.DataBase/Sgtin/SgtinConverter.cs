using System;
using Tools.SqlTransact;

namespace WebService.DataBase.Sgtin
{
    public class SgtinConverter : DataConverter
    {
        public override object ConvertToParametr(object value)
        {
            string[] sgtins = (string[])value;
            var str = "";
            for (var i = 0; i < sgtins.Length; i++)
            {
                if (i < sgtins.Length - 1)
                {
                    str += sgtins[i] + ",";
                }
                else
                {
                    str += sgtins[i];
                }
            }
            return str;
        }

        public override object ConvertToProperty(object value)
        {
            throw new NotImplementedException();
        }
    }
}

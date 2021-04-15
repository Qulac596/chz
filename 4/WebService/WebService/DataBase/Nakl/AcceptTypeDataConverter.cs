using System;
using Tools.SqlTransact;

namespace WebService.DataBase.Nakl
{
    public class AcceptTypeDataConverter : DataConverter
    {
        public override object ConvertToParametr(object value)
        {
            var str = (string)value;

            switch (str)
            {
                case "Прямой":
                    return true;
                    break;
                case "Обратный":
                    return false;
                    break;
                default:
                    return DBNull.Value;
                    break;
            }
        }

        public override object ConvertToProperty(object value)
        {
            if (value == DBNull.Value)
            {
                return "Неопределен";
            }

            var v = (bool)value;

            if (v == true)
            {
                return "Прямой";
            }

            return "Обратный";
        }
    }
}
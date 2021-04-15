using chz.WindowsServices.DataBase;
using chz.WindowsServices.Log;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace chz.DataAccess
{
    public abstract class DataBase<TItem> : IDataBase<TItem> where TItem : DomainObject
    {
        protected string ErrorMessage => "Ошибка при работе с бд.";

        private readonly WindowsServices.Setting.ISettingProvider setting;

        protected DataBase(WindowsServices.Setting.ISettingProvider setting)
        {
            this.setting = setting;
        }

        public abstract IEnumerable<TItem> GetError();

        public abstract void Update(TItem item);

        protected SqlConnection GetSqlConnection()
        {

            var connectString = "Data Source=" + setting.GetValue("BDServerName")
                + ";DataBase=MDLP;Persist Security Info=True;User ID=" + setting.GetValue("ID") +
                ";Password=" + setting.GetValue("Password");

            return new SqlConnection(connectString);
        }

        protected T MapValue<T>(SqlDataReader sqlDataReader, string filedName)
        {
            var value = sqlDataReader[filedName];
            T result;
            result = value == DBNull.Value ? default(T) : (T)value;
            return result;
        }

        protected object Convert(object value)
        {
            if(value == null)
            {
                return DBNull.Value;
            }
            return value;
        }
    }
}

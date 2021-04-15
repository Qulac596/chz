using chz.WindowsServices.DataBase;
using chz.WindowsServices.Setting;
using commoni.Infrastructure;
using System;
using System.Data.SqlClient;

namespace chz.DataAccess.ServiceLog
{
    public class DataBase : IDataBase
    {
        private const string AddMessageSQLReguest = @"Insert Into [dbo].chz_log (service_name, msg_type, msg)
	    Values(@service_name,@msg_type,@msg)";

        private ISettingProvider setting;

        public DataBase(ISettingProvider setting)
        {
            this.setting = setting;
        }

        public void Write(string serviceName, string msgType, string message)
        {
            var con = GetSqlConnection();

            try
            {
                con.Open();

                var command = new SqlCommand(AddMessageSQLReguest, con);
                var parameters = command.Parameters;

                parameters.AddWithValue("@service_name", serviceName);
                parameters.AddWithValue("@msg_type", msgType);
                parameters.AddWithValue("@msg", message);

                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new DataBaseException(e.Message, e);
            }
            finally
            {
                con.Close();
            }
        }

        private SqlConnection GetSqlConnection()
        {

            var connectString = "Data Source=" + setting.GetValue("BDServerName")
                + ";DataBase=MDLP;Persist Security Info=True;User ID=" + setting.GetValue("ID") +
                ";Password=" + setting.GetValue("Password");

            return new SqlConnection(connectString);
        }
    }
}

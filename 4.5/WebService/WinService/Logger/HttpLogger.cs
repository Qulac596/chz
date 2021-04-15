using commoni.Setting;
using System.Text.RegularExpressions;
using Tools.SqlTransact;

namespace WinService.Logger
{
    public static class HttpLogger
    {
        private static SettingProvider setting;
        private static bool isDebug;
        private static string login;
        private static string password;
        private static string connectionString;

        static HttpLogger()
        {
            setting = new SettingProvider();
            isDebug = bool.Parse(setting.GetValue("Debug"));
            login = setting.GetValue("logUser");
            password = setting.GetValue("logPassword");
            connectionString = setting.GetValue("ConnectionString");
        }

        public static void Write(HttpRequest httpRequest, HttpResponse httpResponse)
        {
            if (isDebug == true)
            {
                const string commandText = @"INSERT INTO chz_ui_log
                (request_data,request_host,request_method,request_url,request_body,request_query_string,response_status_code,response_result,login)
                VALUES (@request_data,@request_host,@request_method,@request_url,@request_body,@request_query_string,@response_status_code,@response_result,@login)";

                var parameter = new InsertParameter()
                {
                    Body = httpRequest.Body,
                    Url = httpRequest.Url,
                    Method = httpRequest.Method,
                    QueryString = httpRequest.QueryString,
                    ReguestDateTime = httpRequest.DateTime,
                    Host = httpRequest.Host,
                    Login = httpRequest.Login,
                    StatusCode = httpResponse.StatusCode,
                    Result = httpResponse.Result
                };

                var str = Regex.Replace(connectionString, "{login}", login);

                var conStr = Regex.Replace(str, "{password}", password);

                var transaction = Transact<object>.Create(conStr, commandText, parameter: parameter);
                transaction.ExecuteNonQuery();
            }
        }
    }
}

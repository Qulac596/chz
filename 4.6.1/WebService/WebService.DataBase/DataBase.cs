using System.Text.RegularExpressions;

namespace WebService.DataBase
{
    public abstract class DataBase
    {
        protected string ConnectionString { get; private set; }

        protected DataBase(string connectionStringPattert, string login, string password)
        {
            var str = Regex.Replace(connectionStringPattert, "{login}", login);

            ConnectionString = Regex.Replace(str, "{password}", password);
        }
    }
}
using Tools.SqlTransact;

namespace WebService.DataBase.User
{
    public class GetUserParametr
    {
        [ParamName("login")]
        public string Login { get; set; }

        [ParamName("password")]
        public string Password { get; set; }
    }
}
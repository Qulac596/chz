using Tools.SqlTransact;

namespace WebService.DataBase
{
    public class UserAccessTokenParameter
    {
        [ParamName("access_token")]
        public string AccessToken { get; set; }
    }
}
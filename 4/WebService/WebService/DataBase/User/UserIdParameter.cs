using Tools.SqlTransact;

namespace WebService.DataBase.User
{
    public class UserIdParameter
    {
        [ParamName("chz_user_id")]
        public int UserId { get; set; }
    }
}
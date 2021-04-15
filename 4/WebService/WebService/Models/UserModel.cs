using System;
using Tools.SqlTransact;

namespace WebService.Models
{
    public class UserModel
    {
        [ParamName("chz_user_id")]
        public int Id { get; set; }

        [ParamName("first_name")]
        public string FirstName { get; set; }

        [ParamName("last_name")]
        public string LastName { get; set; }

        [ParamName("login")]
        public string Login { get; set; }

        [ParamName("password")]
        public string Password { get; set; }

        [ParamName("access_token")]
        public string AccessToken { get; set; }

        [ParamName("date_time")]
        public DateTime DateTime { get; set; }

        [ParamName("is_lock")]
        public bool IsLock { get; set; }
    }
}
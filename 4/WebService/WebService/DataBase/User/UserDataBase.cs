using System.Linq;
using Tools.SqlTransact;
using WebService.Models;

namespace WebService.DataBase.User
{
    public class UserDataBase : DataBase, IUserDataBase
    {
        public UserDataBase(string connectionString) : base(connectionString)
        {

        }

        public UserModel Get(string login, string password)
        {
            const string commandText = @"Select chz_user_id,first_name,last_name,login,password,access_token,date_time,is_lock
            From chz_users
            Where is_lock=0 And login=@login And password = @password";

            var parametr = new GetUserParametr() { Login = login, Password = password };
            var transact = Transact<UserModel>.Create(ConnectionString, commandText, parameter: parametr);
            return transact.ExecuteReader().FirstOrDefault();
        }

        public void SaveChanges(UserModel userModel)
        {
            const string commandText = @"Update chz_users
            Set first_name=@first_name,last_name=@last_name,login=@login,password=@password,access_token=@access_token,date_time=@date_time,is_lock=@is_lock
            Where chz_user_id = @chz_user_id";

            var transact = Transact<UserModel>.Create(ConnectionString, commandText, parameter: userModel);
            transact.ExecuteNonQuery();
        }

        public UserModel Get(string accessToken)
        {
            const string commandText = @"Select chz_user_id,first_name,last_name,login,password,access_token,date_time,is_lock
            From chz_users
            Where is_lock=0 And access_token = @access_token";

            var parameter = new UserAccessTokenParameter() { AccessToken = accessToken };
            var transaction = Transact<UserModel>.Create(ConnectionString, commandText, parameter: parameter);
            return transaction.ExecuteReader().FirstOrDefault();
        }

        public void Exit(string accessToken)
        {
            const string commandText = @"Update chz_users
            Set access_token = null
            Where access_token = @access_token";

            var parameter = new UserAccessTokenParameter() { AccessToken = accessToken };
            var transaction = Transact<object>.Create(ConnectionString, commandText, parameter: parameter);
            transaction.ExecuteNonQuery();
        }
    }
}
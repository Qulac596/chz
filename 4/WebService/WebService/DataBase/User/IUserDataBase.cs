using WebService.Models;

namespace WebService.DataBase.User
{
    public interface IUserDataBase
    {
        UserModel Get(string login, string password);

        void SaveChanges(UserModel userModel);

        UserModel Get(string accessToken);

        void Exit(string accessToken);
    }
}
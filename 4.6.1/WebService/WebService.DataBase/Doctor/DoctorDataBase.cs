using System.Linq;
using Tools.SqlTransact;

namespace WebService.DataBase.Doctor
{
    public class DoctorDataBase : DataBase
    {
        public DoctorDataBase(string conString, string login, string password) : base(conString, login, password)
        {

        }

        public Doctor GetUser(int id)
        {
            const string commandText = @"Select chz_user_id,login,password,fio,access_tokent 
            From chz_users
            Where chz_user_id=@chz_user_id";

            var parameter = new IdParameter() { Id = id };
            var transaction = Transact<Doctor>.Create(ConnectionString, commandText, parameter: parameter);
            return transaction.ExecuteReader().FirstOrDefault();
        }

        public Doctor GetUser(string login)
        {
            const string commandText = @"Select doctor_id,fio,doctor_login
            From doctors
            Where doctor_login = @doctor_login;";

            var parameter = new AccountParameter() { Login = login };
            var transaction = Transact<Doctor>.Create(ConnectionString, commandText, parameter: parameter);
            return transaction.ExecuteReader().FirstOrDefault();
        }

        public void SaveChanges(Doctor user)
        {
            const string commandText = @"Update chz_users
            Set fio=@fio,login=@login,password=@password,access_tokent=@access_tokent
            Where chz_user_id=@chz_user_id";

            var transaction = Transact<object>.Create(ConnectionString, commandText, parameter: user);
            transaction.ExecuteNonQuery();
        }
    }
}
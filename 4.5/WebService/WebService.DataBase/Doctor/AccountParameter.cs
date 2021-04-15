using Tools.SqlTransact;

namespace WebService.DataBase.Doctor
{
    public class AccountParameter
    {
        [ParamName("doctor_login")]
        public string Login { get; set; }
    }
}
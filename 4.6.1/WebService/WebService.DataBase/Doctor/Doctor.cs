using Tools.SqlTransact;

namespace WebService.DataBase.Doctor
{
    public class Doctor
    {
        [ParamName("doctor_id")]
        public int Id { get; set; }

        [ParamName("fio")]
        public string Fio { get; set; }

        [ParamName("doctor_login")]
        public string Login { get; set; }
    }
}
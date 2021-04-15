using Tools.SqlTransact;

namespace WebService.DataBase.Doctor
{
    public class IdParameter
    {
        [ParamName("doctor_id")]
        public int Id { get; set; }
    }
}
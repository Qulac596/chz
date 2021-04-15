using Tools.SqlTransact;

namespace WebService.DataBase.RecesiveType
{
    public class RecesiveType
    {
        [ParamName("chz_receive_type_id")]
        public int Id { get; set; }

        [ParamName("value")]
        public string Value { get; set; }

        [ParamName("is_default")]
        public bool IsDefault { get; set; }
    }
}

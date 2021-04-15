using Tools.SqlTransact;

namespace WebService.DataBase.NdsValue
{
    public class NdsValue
    {
        [ParamName("chz_nds_value_id")]
        public int Id { get; set; }

        [ParamName("value")]
        public int? Value { get; set; }

        [ParamName("is_default")]
        public bool IsDefault { get; set; }
    }
}

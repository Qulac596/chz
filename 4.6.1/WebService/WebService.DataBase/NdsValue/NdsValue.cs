using Tools.SqlTransact;

namespace WebService.DataBase.NdsValue
{
    public class NdsValue
    {
        [ParamName("value")]
        public int? Value { get; set; }

        [ParamName("is_default")]
        public bool IsDefault { get; set; }
    }
}

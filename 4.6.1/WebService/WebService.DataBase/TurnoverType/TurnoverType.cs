using Tools.SqlTransact;

namespace WebService.DataBase.TurnoverType
{
    public class TurnoverType
    {
        [ParamName("chz_turnover_tupe_id")]
        public int Id { get; set; }

        [ParamName("value")]
        public string Value { get; set; }

        [ParamName("is_default")]
        public bool IsDefault { get; set; }
    }
}

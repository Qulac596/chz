using Tools.SqlTransact;

namespace WebService.DataBase.SourceType
{
    public class SourceType
    {
        [ParamName("chz_source_type_id")]
        public int Id { get; set; }

        [ParamName("value")]
        public string Value { get; set; }

        [ParamName("is_default")]
        public bool IsDefault { get; set; }
    }
}

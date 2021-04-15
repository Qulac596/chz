using Tools.SqlTransact;

namespace WebService.DataBase.SgtinStatus
{
    public class SgtinStatus
    {
        [ParamName("chz_scaned_sgtin_status_id")]
        public int Id { get; set; }

        [ParamName("value")]
        public string Value { get; set; }

        [ParamName("style")]
        public string Style { get; set; }
    }
}

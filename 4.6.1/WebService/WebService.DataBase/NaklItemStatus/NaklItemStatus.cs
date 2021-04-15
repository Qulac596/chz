using Tools.SqlTransact;

namespace WebService.DataBase.NaklItemStatus
{
    public class NaklItemStatus
    {
        [ParamName("chz_nakl_item_status_id")]
        public int Id { get; set; }

        [ParamName("value")]
        public string Value { get; set; }

        [ParamName("style")]
        public string Style { get; set; }
    }
}

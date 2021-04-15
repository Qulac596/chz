using Tools.SqlTransact;

namespace WebService.DataBase.Sgtin
{
    public class AddSgtinResult
    {
        [ParamName("sgtin", 1024)]
        public string Sgtin { get; set; }

        [ParamName("gtin", 1024)]
        public string Gtin { get; set; }

        [ParamName("chz_nakl_item_id", 1024)]
        public int NaklItemId { get; set; }

        [ParamName("status", 1024)]
        public string Status { get; set; }

        [ParamName("style", 1024)]
        public string Style { get; set; }

        [ParamName("gtin_same_as_current", 1024)]
        public string GtinSameAsCurrent { get; set; }
    }
}

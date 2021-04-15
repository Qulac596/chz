using Tools.SqlTransact;

namespace WebService.DataBase.Sgtin
{
    class NaklItemGtinParameter
    {
        [ParamName("gtin")]
        public string Gtin { get; set; }

        [ParamName("chz_nakl_id")]
        public int NaklId { get; set; }

        [ParamName("name")]
        public string Name { get; set; }
    }
}

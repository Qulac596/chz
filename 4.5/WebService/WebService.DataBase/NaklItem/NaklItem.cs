using Tools.SqlTransact;

namespace WebService.DataBase.NaklItem
{
    public class NaklItem
    {
        [ParamName("chz_nakl_item_id")]
        public int Id { get; set; }

        [ParamName("name")]
        public string Name { get; set; }

        [ParamName("count")]
        public int Count { get; set; }

        [ParamName("code_count")]
        public int CodeCount { get; set; }

        [ParamName("nds")]
        public int Nds { get; set; }

        [ParamName("cost")]
        public decimal Cost { get; set; }

        [ParamName("vat_value")]
        public decimal VatValue { get; set; }

        [ParamName("sum")]
        public decimal Sum { get; set; }

        [ParamName("status")]
        public string Status { get; set; }

        [ParamName("style")]
        public string Style { get; set; }
    }
}

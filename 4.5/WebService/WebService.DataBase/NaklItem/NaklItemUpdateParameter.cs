using Tools.SqlTransact;

namespace WebService.DataBase.NdsValue
{
    public class NaklItemUpdateParameter
    {
        [ParamName("chz_nakl_item_id")]
        public int Id { get; set; }

        [ParamName("cost")]
        public decimal Cost { get; set; }

        [ParamName("vat_value")]
        public decimal VatValue { get; set; }

        [ParamName("nds")]
        public int? Nds { get; set; }
    }
}

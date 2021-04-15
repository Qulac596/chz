using Tools.SqlTransact;

namespace WebService.Models
{
    public class CreateNaklItemModel
    {
        [ParamName("chz_gtin_id")]
        public int Id { get; set; }

        [ParamName("chz_nakl_id")]
        public int NaklId { get; set; }

        [ParamName("count")]
        public int Count { get; set; }

        [ParamName("const")]
        public decimal Const { get; set; }

        [ParamName("vat_value")]
        public decimal VatValue { get; set; }

        [ParamName("prod_sell_name")]
        public string ProdSellName { get; set; }
    }
}
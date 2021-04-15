using Tools.SqlTransact;

namespace WebService.Models
{
    public class NaklItemModel
    {
        [ParamName("chz_gtin_id")]
        public int Id { get; set; }

        [ParamName("sell_name")]
        public string Name { get; set; }

        [ParamName("sgtin_count")]
        public int Count { get; set; }

        [ParamName("price")]
        public decimal Price { get; set; }

        [ParamName("nds")]
        public decimal Nds { get; set; }

        [ParamName("sum")]
        public decimal Sum { get; set; }

        [ParamName("status")]
        public string Status { get; set; }

        [ParamName("color")]
        public string Color { get; set; }
    }
}
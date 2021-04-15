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

        [ParamName("nds")]
        public decimal Nds { get; set; }

        [ParamName("price")]
        public decimal Price { get; set; }

        [ParamName("sum")]
        public decimal Sum { get; set; }

        [ParamName("scaned_count")]
        public int ScenedCount { get; set; }

        [ParamName("count_matched")]
        public int CountMatched { get; set; }
    }
}

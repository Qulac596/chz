namespace WebService.ViewModel
{
    public class NaklItemViewModel
    {
        public int nakl_item_id { get; set; }

        public string name { get; set; }

        public int count { get; set; }

        public int code_count { get; set; }

        public string validation { get; set; }

        public decimal cost { get; set; }

        public decimal vat_value { get; set; }

        public int nds { get; set; }

        public decimal sum { get; set; }

        public string status { get; set; }

        public string style { get; set; }
    }
}
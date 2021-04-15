using System;

namespace WebService.ViewModel
{
    public class NaklGridViewModel
    {
        public int nakl_id { get; set; }

        public string doc_num { get; set; }

        public DateTime doc_date { get; set; }

        public string provider { get; set; }

        public string acceptance_type { get; set; }

        public string contract_type { get; set; }

        public decimal sum { get; set; }

        public string status { get; set; }

        public string status_style { get; set; }
    }
}
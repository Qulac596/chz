using System;

namespace WebService.ViewModel
{
    public class NaklFullViewModel
    {
        public int nakl_id { get; set; }

        public int provider_id { get; set; }

        public int company_id { get; set; }

        public DateTime operation_date { get; set; }

        public DateTime doc_date { get; set; }

        public string doc_num { get; set; }

        public int receive_type_id { get; set; }

        public int source_type_id { get; set; }

        public int contract_type_id { get; set; }

        public int turnover_type_id { get; set; }

        public string contract_num { get; set; }
    }
}
using System;

namespace WebService.ViewModel
{
    public class NaklViewModel
    {
        public int nakl_id { get; set; }

        public string doc_num { get; set; }

        public DateTime doc_date { get; set; }

        public string provider_inn { get; set; }

        public string provider_name { get; set; }

        public string provider_address { get; set; }

        public string receiver_inn { get; set; }

        public string receiver_name { get; set; }

        public string receiver_address { get; set; }

        public int nakl_status_id { get; set; }

        public string acceptance_type { get; set; }

        public string provider_tooltip { get; set; }

        public string receiver_tooltip { get; set; }

        public string form_caption { get; set; }
    }
}
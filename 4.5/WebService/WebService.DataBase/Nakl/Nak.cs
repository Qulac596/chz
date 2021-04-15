using System;
using Tools.SqlTransact;

namespace WebService.DataBase.Nakl
{
    public class Nak
    {
        [ParamName("chz_nakl_id")]
        public int nakl_id { get; set; }

        [ParamName("doc_num")]
        public string doc_num { get; set; }

        [ParamName("doc_data")]
        public DateTime date { get; set; }

        [ParamName("provider_inn")]
        public string provider_inn { get; set; }

        [ParamName("provider_name")]
        public string provider_name { get; set; }

        [ParamName("provider_address")]
        public string provider_address { get; set; }

        [ParamName("receiver_inn")]
        public string receiver_inn { get; set; }

        [ParamName("receiver_name")]
        public string receiver_name { get; set; }

        [ParamName("receiver_address")]
        public string receiver_address { get; set; }

        [ParamName("nakl_status_id")]
        public int nakl_status_id { get; set; }

        [ParamName("acept_type")]
        public string acceptance_type { get; set; }
    }
}

using System;
using Tools.SqlTransact;

namespace WebService.DataBase.Nakl
{
    public class NaklFull
    {
        [ParamName("chz_nakl_id")]
        public int nakl_id { get; set; }

        [ParamName("provider_id")]
        public int provider_id { get; set; }

        [ParamName("company_id")]
        public int company_id { get; set; }

        [ParamName("receiver_id")]
        public int receiver_id { get; set; }

        [ParamName("operation_data")]
        public DateTime operation_date { get; set; }

        [ParamName("doc_data")]
        public DateTime doc_date { get; set; }

        [ParamName("doc_num")]
        public string doc_num { get; set; }

        [ParamName("chz_receive_type_id")]
        public int receive_type_id { get; set; }

        [ParamName("chz_source_type_id")]
        public int source_type_id { get; set; }

        [ParamName("chz_contract_type_id")]
        public int contract_type_id { get; set; }

        [ParamName("chz_turnover_tupe_id")]
        public int turnover_type_id { get; set; }

        [ParamName("contract_num")]
        public string contract_num { get; set; }

    }
}

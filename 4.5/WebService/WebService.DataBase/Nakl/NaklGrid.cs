using System;
using Tools.SqlTransact;

namespace WebService.DataBase.Nakl
{
    public class NaklGrid
    {
        [ParamName("chz_nakl_id")]
        public int Id { get; set; }

        [ParamName("doc_num")]
        public string DocNum { get; set; }

        [ParamName("doc_data")]
        public DateTime DateTime { get; set; }

        [ParamName("provider_name")]
        public string Povider { get; set; }

        [ParamName("accept_types_value")]
        public string AcceptanceType { get; set; }

        [ParamName("contract_types_value")]
        public string ContractType { get; set; }

        [ParamName("sum")]
        public decimal Sum { get; set; }

        [ParamName("nakl_statuses_value")]
        public string Status { get; set; }

        [ParamName("class_name")]
        public string StatusStule { get; set; }
    }
}

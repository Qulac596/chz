using System;
using Tools.SqlTransact;
using WebService.DataBase.Nakl;

namespace WebService.Models
{
    public class NaklGridModel
    {
        [ParamName("chz_nakl_id")]
        public int Id { get; set; }

        [ParamName("doc_num")]
        public string DocNum { get; set; }

        [ParamName("doc_date")]
        public DateTime DocDate { get; set; }

        [ParamName("is_direct")]
        [DataConverterAttribyte(typeof(AcceptTypeDataConverter))]
        public string Accept { get; set; }

        [ParamName("status_name")]
        public string Status { get; set; }

        [ParamName("pic_url")]
        public string StatusPicUrl { get; set; }

        [ParamName("contract_type_name")]
        public string ContractType { get; set; }

        [ParamName("org_name")]
        public string Provider { get; set; }

        [ParamName("sum")]
        public decimal Sum { get; set; }
    }
}
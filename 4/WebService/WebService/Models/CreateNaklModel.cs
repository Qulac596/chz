using System;
using Tools.SqlTransact;

namespace WebService.Models
{
    public class CreateNaklModel
    {
        [ParamName("chz_nakl_id")]
        public int Id { get; set; }

        [ParamName("subject_id")]
        public int SubjectId { get; set; }

        [ParamName("shipper_id")]
        public int ShipperId { get; set; }

        [ParamName("operation_date")]
        public DateTime OperationDate { get; set; }

        [ParamName("doc_num")]
        public string DocNum { get; set; }

        [ParamName("doc_date")]
        public DateTime DocDate { get; set; }

        [ParamName("receive_type")]
        public int ReceiveType { get; set; }

        [ParamName("source")]
        public int Source { get; set; }

        [ParamName("contract_type")]
        public int ContractType { get; set; }

        [ParamName("contract_num")]
        public string ContractNum { get; set; }
    }
}
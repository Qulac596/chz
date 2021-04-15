using Tools.SqlTransact;

namespace WebService.DataBase.Nakl
{
    public class NaklUpdateParameter
    {
        [ParamName("chz_nakl_id")]
        public int Id { get; set; }

        [ParamName("chz_provider_id")]
        public int ProviderId { get; set; }

        [ParamName("chz_contract_type_id")]
        public int ContractTypeId { get; set; }

        [ParamName("chz_nakl_status_id")]
        public int StatusId { get; set; }
    }
}
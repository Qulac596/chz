using Tools.SqlTransact;

namespace WebService.DataBase.ContractType
{
    public class ContractType
    {
        [ParamName("chz_contract_type_id")]
        public int Id { get; set; }

        [ParamName("value")]
        public string Value { get; set; }

        [ParamName("is_default")]
        public bool IsDefault { get; set; }
    }
}

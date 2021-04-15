using Tools.SqlTransact;

namespace WebService.Models
{
    public class ContractTypeModel
    {
        [ParamName("chz_contract_type_id")]
        public int Id { get; set; }

        [ParamName("name")]
        public string Name { get; set; }
    }
}
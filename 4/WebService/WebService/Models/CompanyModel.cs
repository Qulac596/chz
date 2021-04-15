using Tools.SqlTransact;

namespace WebService.Models
{
    public class CompanyModel
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string INN { get; set; }

        public AddressModel[] AddressModels { get; set; }
    }
}
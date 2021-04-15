using Tools.SqlTransact;

namespace WebService.DataBase.Companies
{
    public class CompanyIdParameter
    {
        [ParamName("chz_company_id")]
        public int Id { get; set; }
    }
}

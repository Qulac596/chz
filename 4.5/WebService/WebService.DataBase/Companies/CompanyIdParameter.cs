using Tools.SqlTransact;

namespace WebService.DataBase.Companies
{
    public class CompanyIdParameter
    {
        [ParamName("chz_company_id",100,System.Data.DbType.Int32)]
        public int Id { get; set; }
    }
}

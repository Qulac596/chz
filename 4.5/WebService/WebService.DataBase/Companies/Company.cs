using Tools.SqlTransact;

namespace WebService.DataBase.Companies
{
    public class Company
    {
        [ParamName("chz_company_id")]
        public int Id { get; set; }

        [ParamName("name")]
        public string Name { get; set; }

        [ParamName("inn")]
        public string Inn { get; set; }

        [ParamName("short_name")]
        public string ShortName { get; set; }
    }
}

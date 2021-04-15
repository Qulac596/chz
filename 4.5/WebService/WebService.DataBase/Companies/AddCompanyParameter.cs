using Tools.SqlTransact;

namespace WebService.DataBase.Companies
{
    class AddCompanyParameter
    {
        [ParamName("name")]
        public string Name { get; set; }

        [ParamName("inn")]
        public string Inn { get; set; }

        [ParamName("short_name")]
        public string ShortName { get; set; }
    }
}

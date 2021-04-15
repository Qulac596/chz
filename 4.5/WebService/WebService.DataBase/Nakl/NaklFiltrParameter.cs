using Tools.SqlTransact;

namespace WebService.DataBase.Nakl
{
    class NaklFiltrParameter
    {
        [ParamName("company_id")]
        public int? CompanyId { get; set; }

        [ParamName("year")]
        public int? Year { get; set; }

        [ParamName("month")]
        public int? Month { get; set; }

        [ParamName("statusId")]
        public int? StatusId { get; set; }
    }
}

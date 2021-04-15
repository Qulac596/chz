using Tools.SqlTransact;

namespace WebService.DataBase.NaklItem
{
    public class NaklItemSenOneParameter
    {
        [ParamName("chz_gtin_id")]
        public int Id { get; set; }

        [ParamName("gtin_status_id")]
        public int Status { get; set; }
    }
}
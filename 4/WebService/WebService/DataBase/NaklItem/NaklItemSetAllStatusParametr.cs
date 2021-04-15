using Tools.SqlTransact;

namespace WebService.DataBase.NaklItem
{
    public class NaklItemSetAllStatusParametr
    {
        [ParamName("chz_nakl_id")]
        public int NaklId { get; set; }

        [ParamName("gtin_status_id")]
        public int Status { get; set; }
    }
}
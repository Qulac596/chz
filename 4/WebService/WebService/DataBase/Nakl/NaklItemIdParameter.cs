using Tools.SqlTransact;

namespace WebService.DataBase.Nakl
{
    public class NaklItemIdParameter
    {
        [ParamName("chz_gtin_id")]
        public int Id { get; set; }
    }
}
using Tools.SqlTransact;

namespace WebService.DataBase.Nakl
{
    class SetNaklStatusParameter
    {
        [ParamName("chz_nakl_id")]
        public int NaklId { get; set; }

        [ParamName("chz_nakl_status_id")]
        public int NaklStatusId { get; set; }
    }
}

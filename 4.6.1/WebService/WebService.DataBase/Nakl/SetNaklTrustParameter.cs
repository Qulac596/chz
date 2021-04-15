using Tools.SqlTransact;

namespace WebService.DataBase.Nakl
{
    class SetNaklTrustParameter
    {
        [ParamName("chz_nakl_id")]
        public int Id { get; set; }

        [ParamName("trust")]
        public bool? IsTrust { get; set; }
    }
}

using Tools.SqlTransact;

namespace WebService.DataBase.NaklStatus
{
    public class NaklStatus
    {
        [ParamName("chz_nakl_status_id")]
        public int Id { get; set; }

        [ParamName("value")]
        public string Value { get; set; }

        [ParamName("class_name")]
        public string ClassName { get; set; }
    }
}

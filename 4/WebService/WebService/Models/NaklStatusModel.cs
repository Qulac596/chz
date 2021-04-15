using Tools.SqlTransact;

namespace WebService.Models
{
    public class NaklStatusModel
    {
        [ParamName("chz_nakl_status_id")]
        public int Id { get; set; }

        [ParamName("chz_nakl_status_name")]
        public string Name { get; set; }
    }
}
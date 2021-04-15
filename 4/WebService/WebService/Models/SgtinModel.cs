using Tools.SqlTransact;

namespace WebService.Models
{
    public class SgtinModel
    {
        [ParamName("chz_sgtin_id")]
        public int Id { get; set; }

        [ParamName("sgtin")]
        public string sgtin { get; set; }
    }
}
using Tools.SqlTransact;

namespace WebService.Models
{
    public class NaklModel
    {
        [ParamName("chz_nakl_id")]
        public int Id { get; set; }

        [ParamName("provider_name")]
        public string ProviderName { get; set; }

        [ParamName("provider_inn")]
        public string ProviderInn { get; set; }

        [ParamName("provider_address")]
        public string ProviderAddress { get; set; }

        [ParamName("recesiver_name")]
        public string RecesiverName { get; set; }

        [ParamName("recesiver_inn")]
        public string RecesiverInn { get; set; }

        [ParamName("recesiver_address")]
        public string RecesiverAddress { get; set; }
    }
}
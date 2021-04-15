using Tools.SqlTransact;

namespace WebService.DataBase.Sgtin
{
    public class SgtinResult
    {
        [ParamName("provider_sgtin")]
        public string ProviderSgtin { get; set; }

        [ParamName("recesiver_sgtin")]
        public string ReceiverSgtin { get; set; }
    }
}

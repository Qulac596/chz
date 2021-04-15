using Tools.SqlTransact;

namespace WebService.Models
{
    public class AddressModel
    {
        [ParamName("chz_address_id")]
        public int Id { get; set; }

        [ParamName("text")]
        public string Text { get; set; }
    }
}
using Tools.SqlTransact;

namespace WebService.DataBase.Address
{
    public class Address
    {
        [ParamName("chz_address_id")]
        public int Id { get; set; }

        [ParamName("text")]
        public string Text { get; set; }
    }
}

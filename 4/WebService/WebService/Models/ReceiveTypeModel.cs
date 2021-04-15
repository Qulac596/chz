using Tools.SqlTransact;

namespace WebService.Models
{
    public class ReceiveTypeModel
    {
        [ParamName("chz_receive_type_id")]
        public int Id { get; set; }

        [ParamName("name")]
        public string Name { get; set; }
    }
}
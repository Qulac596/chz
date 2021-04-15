using Tools.SqlTransact;

namespace WebService.DataBase.AcceptType
{
    public class AcceptType
    {
        [ParamName("chz_nakls__list_of_accept_type_id")]
        public int Id { get; set; }

        [ParamName("value")]
        public string Value { get; set; }
    }
}

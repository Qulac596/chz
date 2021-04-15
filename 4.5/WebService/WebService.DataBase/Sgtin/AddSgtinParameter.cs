using Tools.SqlTransact;

namespace WebService.DataBase.Sgtin
{
    public class AddSgtinParameter
    {
        [ParamName("chz_nakl_id")]
        public int? NaklId { get; set; }

        [ParamName("chz_nakl_item_id")]
        public int? NaklItemId { get; set; }

        [ParamName("sgtins_list")]
        [DataConverterAttribyte(typeof(SgtinConverter))]
        public string[] Sgtins { get; set; }
    }
}

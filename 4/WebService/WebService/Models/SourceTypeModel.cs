using Tools.SqlTransact;

namespace WebService.Models
{
    public class SourceTypeModel
    {
        [ParamName("chz_source_type_id")]
        public int Id { get; set; }

        [ParamName("name")]
        public string Name { get; set; }
    }
}
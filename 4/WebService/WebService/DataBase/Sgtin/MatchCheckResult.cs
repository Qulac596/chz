using Tools.SqlTransact;

namespace WebService.DataBase.Sgtin
{
    public class MatchCheckResult
    {
        [ParamName("count")]
        public int Count { get; set; }

        [ParamName("match_count")]
        public int MatchCount { get; set; }
    }
}
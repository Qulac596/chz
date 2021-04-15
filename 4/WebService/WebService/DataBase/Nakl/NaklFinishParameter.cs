using System;
using Tools.SqlTransact;

namespace WebService.DataBase.Nakl
{
    public class NaklFinishParameter
    {
        [ParamName("chz_nakl_id")]
        public int NaklId { get; set; }

        [ParamName("date_time_finish")]
        public DateTime DateTime { get; set; }
    }
}
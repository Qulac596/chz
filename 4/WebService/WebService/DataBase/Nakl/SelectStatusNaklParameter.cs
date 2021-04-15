using System;
using Tools.SqlTransact;

namespace WebService.DataBase.Nakl
{
    public class SelectStatusNaklParameter
    {
        [ParamName("startDateTime")]
        public DateTime StartDateTime { get; set; }

        [ParamName("endDateTime")]
        public DateTime EndDateTime { get; set; }

        [ParamName("chz_address_id")]
        public int AddressId { get; set; }

        [ParamName("chz_nakl_status_id")]
        public int StatudId { get; set; }
    }
}
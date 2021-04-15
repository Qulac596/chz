using System;
using Tools.SqlTransact;

namespace WebService.DataBase.Nakl
{
    public class SelectAllNaklParametr
    {
        [ParamName("startDateTime")]
        public DateTime StartDateTime { get; set; }

        [ParamName("endDateTime")]
        public DateTime EndDateTime { get; set; }

        [ParamName("chz_address_id")]
        public int AddressId { get; set; }
    }
}
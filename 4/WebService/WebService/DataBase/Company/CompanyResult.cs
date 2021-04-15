using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tools.SqlTransact;

namespace WebService.DataBase.Company
{
    public class CompanyResult
    {
        [ParamName("chz_company_id")]
        public int companyId { get; set; }

        [ParamName("org_name")]
        public string Name { get; set; }

        [ParamName("inn")]
        public string INN { get; set; }

        [ParamName("chz_address_id")]
        public int addressId { get; set; }

        [ParamName("text")]
        public string Text { get; set; }
    }
}
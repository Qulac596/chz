using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebService.Models;

namespace WebService.DataBase.Company
{
    public interface ICompanyDataBase
    {
        IEnumerable<CompanyResult> Get();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebService.Models;

namespace WebService.DataBase.RecesiverType
{
    public interface IRecesiverTypeDataBase
    {
        IEnumerable<ReceiveTypeModel> Get();
    }
}
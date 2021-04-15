using System.Collections.Generic;
using WebService.Models;

namespace WebService.DataBase.SourceType
{
    public interface ISourceTypeDataBase
    {
        IEnumerable<SourceTypeModel> Get();
    }
}
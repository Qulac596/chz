using System.Collections.Generic;

namespace chz.WindowsServices.DirectoryLoader.DataBase
{
    public interface IDataBase : WindowsServices.DataBase.IDataBase<Request>
    {
       IEnumerable<Request> GetNewRequest();
      
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using WebService.DataBase.RecesiveType;
using WebService.ViewModel;

namespace WebService.Services.ReferenceBook
{
    public class RecesiveTypesService : ServiceBase
    {
        public RecesiveTypesService(string connectionStringPattern, string login, string password) : base(connectionStringPattern, login, password) { }

        public Result<IEnumerable<ReceiveTypeViewModel>> Get()
        {
            try
            {
                var recesiveTypeDataBase = new RecesiveTypeDataBase(ConnectionStringPattern, Login, Password);
                return new Result<IEnumerable<ReceiveTypeViewModel>>(recesiveTypeDataBase.GetAll()
                .Select((x) => new ReceiveTypeViewModel() { receive_type_id = x.Id, value = x.Value, is_default = x.IsDefault })
                .ToList());
            }
            catch (Exception e)
            {
                return new Result<IEnumerable<ReceiveTypeViewModel>>(e.Message);
            }
        }
    }
}

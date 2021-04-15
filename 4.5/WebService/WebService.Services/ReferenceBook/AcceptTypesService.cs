using System;
using System.Collections.Generic;
using System.Linq;
using WebService.DataBase.AcceptType;
using WebService.ViewModel;

namespace WebService.Services.ReferenceBook
{
    public class AcceptTypesService : ServiceBase
    {
        public AcceptTypesService(string connectionStringPattern, string login, string password) : base(connectionStringPattern, login, password) { }

        public Result<IEnumerable<AcceptTypeViewModel>> GetAll()
        {
            try
            {
                var acceptTypeDataBase = new AcceptTypeDataBase(ConnectionStringPattern, Login, Password);
                return new Result<IEnumerable<AcceptTypeViewModel>>(
                    acceptTypeDataBase.GetAll()
                    .Select((x) => new AcceptTypeViewModel() { accept_type_id = x.Id, value = x.Value })
                    .ToList());
            }
            catch (Exception e)
            {
                return new Result<IEnumerable<AcceptTypeViewModel>>(e.Message);
            }
        }
    }
}

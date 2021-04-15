using System;
using System.Collections.Generic;
using System.Linq;
using WebService.DataBase.NdsValue;
using WebService.ViewModel;

namespace WebService.Services.ReferenceBook
{
    public class NdsValuesService : ServiceBase
    {
        public NdsValuesService(string connectionStringPattern, string login, string password) : base(connectionStringPattern, login, password) { }

        public Result<IEnumerable<NdsValueViewModel>> Get()
        {
            try
            {
                var ndsValueDataBase = new NdsValueDataBase(ConnectionStringPattern, Login, Password);
                return new Result<IEnumerable<NdsValueViewModel>>(
                   ndsValueDataBase.GetAll()
                .Select((x) => new NdsValueViewModel() { value = x.Value, is_default = x.IsDefault })
                .ToList());
            }
            catch (Exception e)
            {
                return new Result<IEnumerable<NdsValueViewModel>>(e.Message);
            }
        }
    }
}

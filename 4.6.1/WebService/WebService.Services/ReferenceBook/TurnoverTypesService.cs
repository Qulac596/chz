using System;
using System.Collections.Generic;
using System.Linq;
using WebService.DataBase.TurnoverType;
using WebService.ViewModel;

namespace WebService.Services.ReferenceBook
{
    public class TurnoverTypesService : ServiceBase
    {
        public TurnoverTypesService(string connectionStringPattern, string login, string password) : base(connectionStringPattern, login, password) { }

        public Result<IEnumerable<TurnoverTypeViewModel>> Get()
        {
            try
            {
                var turnverTypeDataBase = new TurnverTypeDataBase(ConnectionStringPattern, Login, Password);
                return new Result<IEnumerable<TurnoverTypeViewModel>>(turnverTypeDataBase.GetAll().
                Select((x) => new TurnoverTypeViewModel() { turnover_type_id = x.Id, value = x.Value, is_default = x.IsDefault })
                .ToList());
            }
            catch (Exception e)
            {
                return new Result<IEnumerable<TurnoverTypeViewModel>>(e.Message);
            }
        }
    }
}

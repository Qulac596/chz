using System;
using System.Collections.Generic;
using System.Linq;
using WebService.DataBase.SourceType;
using WebService.ViewModel;

namespace WebService.Services.ReferenceBook
{
    public class SourceTypesService : ServiceBase
    {
        public SourceTypesService(string connectionStringPattern, string login, string password) : base(connectionStringPattern, login, password) { }

        public Result<IEnumerable<SourceTypeViewModel>> Get()
        {
            try
            {
                var sourceTypeDataBase = new SourceTypeDataBase(ConnectionStringPattern, Login, Password);
                return new Result<IEnumerable<SourceTypeViewModel>>(sourceTypeDataBase.GetAll()
                .Select((x) => new SourceTypeViewModel() { source_type_id = x.Id, value = x.Value, is_default = x.IsDefault })
                .ToList());
            }
            catch (Exception e)
            {
                return new Result<IEnumerable<SourceTypeViewModel>>(e.Message);
            }
        }
    }
}

using System;
using WebService.DataBase.NaklItem;
using WebService.DataBase.NdsValue;
using WebService.ViewModel;
using System.Collections.Generic;
using System.Linq;

namespace WebService.Services
{
    public class NaklItemService : ServiceBase
    {
        public NaklItemService(string connectionStringPattern, string login, string password) : base(connectionStringPattern, login, password) { }


        public Result<IEnumerable<NaklItemViewModel>> Get(int naklId)
        {
            try 
            {
                var naklItemDataBase = new NaklItemDataBase(ConnectionStringPattern, Login, Password);
                return new Result<IEnumerable<NaklItemViewModel>>(naklItemDataBase.Get(naklId).Select((x) => new NaklItemViewModel()
                {
                    nakl_item_id=x.Id,
                    name=x.Name,
                    nds=x.Nds,
                    code_count=x.CodeCount,
                    cost=x.Cost,
                    status =x.Status,
                    count=x.Count,
                    vat_value=x.VatValue,
                    sum=x.Sum,
                    style=x.Style,
                    validation="Нет"
                }));
            }catch(Exception e) 
            {
                return new Result<IEnumerable<NaklItemViewModel>>(e.Message);
            }
        }

        public Result<object> Update(int nakl_item_id, decimal cost, int nds_value_id)
        {
            try
            {
                var ndsDataBase = new NdsValueDataBase(ConnectionStringPattern, Login, Password);
                var ndsValue = ndsDataBase.GetById(nds_value_id);
                if (ndsValue != null)
                {
                    var nds = ndsValue.Value == null ? 0 : ndsValue.Value.Value;

                    var vat_value = (cost * nds) / (100 + nds);

                    var naklItemDataBase = new NaklItemDataBase(ConnectionStringPattern, Login, Password);
                    naklItemDataBase.Update(nakl_item_id, cost, vat_value,ndsValue.Value);
                    return new Result<object>();
                }
                else
                {
                    return new Result<object>("Значение НДС с таким id не найдено.");
                }
            }
            catch (Exception e)
            {
                return new Result<object>(e.Message);
            }
        }
    }
}

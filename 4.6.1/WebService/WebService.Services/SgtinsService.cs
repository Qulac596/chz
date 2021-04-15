using System;
using System.Collections.Generic;
using System.Linq;
using WebService.DataBase.Sgtin;
using WebService.DataBase.SgtinStatus;
using WebService.ViewModel;

namespace WebService.Services
{
    public class SgtinsService : ServiceBase
    {
        public SgtinsService(string connectionStringPattern, string login, string password) : base(connectionStringPattern, login, password) { }

        public Result<IEnumerable<SgtinViewModel>> Get(int id)
        {
            try
            {
                var sgtinDataBase = new SgtinDataBase(ConnectionStringPattern, Login, Password);
                var naklAcceptTypeId = sgtinDataBase.GetNaklAcceptTypeId(id);
                var sgtinStatusDataBase = new SgtinStatusDataBase(ConnectionStringPattern, Login, Password);
                var statuses = sgtinStatusDataBase.GetAll();

                if (naklAcceptTypeId == 1)
                {
                    return new Result<IEnumerable<SgtinViewModel>>(sgtinDataBase.GetStinResult(id).Select((x) =>
                    {
                        var s = new SgtinViewModel() { };

                        if (x.ProviderSgtin != null && x.ReceiverSgtin == null)
                        {
                            //не проверен
                            s.status = statuses.First((z) => z.Id == 3).Value;
                            s.style = statuses.First((z) => z.Id == 3).Style;
                            s.sgtin = x.ProviderSgtin;
                        }
                        else if (x.ProviderSgtin == null && x.ReceiverSgtin != null)
                        {
                            //лишний
                            s.status = statuses.First((z) => z.Id == 4).Value;
                            s.style = statuses.First((z) => z.Id == 4).Style;
                            s.sgtin = x.ReceiverSgtin;
                        }
                        else if (x.ProviderSgtin != null & x.ReceiverSgtin != null)
                        {
                            //совпадает
                            s.status = statuses.First((z) => z.Id == 2).Value;
                            s.style = statuses.First((z) => z.Id == 2).Style;
                            s.sgtin = x.ProviderSgtin;
                        }
                        return s;
                    }).ToList());
                }
                else
                {
                    return new Result<IEnumerable<SgtinViewModel>>(sgtinDataBase.Get(id).Select((x) => new SgtinViewModel()
                    {
                        sgtin = x.Value,
                        status = statuses.First((z) => z.Id == 1).Value,
                        style = statuses.First((z) => z.Id == 1).Style
                    }));
                }
            }
            catch (Exception e)
            {
                return new Result<IEnumerable<SgtinViewModel>>(e.Message);
            }
        }

        public Result<object> Post(int? id, string[] sgtins, int? nakl_item_id = null)
        {
            try
            {
                var sgtinDataBase = new SgtinDataBase(ConnectionStringPattern, Login, Password);

                return new Result<object>(
                    sgtinDataBase.AddNewSgtin(naklId: id, naklItemId: nakl_item_id, sgtins: sgtins)
                    .Select((x) => new AddSgtinResultViewModel()
                    {
                        sgtin = x.Sgtin,
                        gtin = x.Gtin,
                        status = x.Status,
                        nakl_item_id = x.NaklItemId,
                        gtin_same_as_current = x.GtinSameAsCurrent,
                        style = x.Style
                    })
                    );
            }
            catch (Exception e)
            {
                return new Result<object>(e.Message);
            }
        }
    }
}

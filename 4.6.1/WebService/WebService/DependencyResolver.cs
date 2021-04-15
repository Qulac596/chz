using commoni.Setting;
using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using WebService.Controllers;
using System.Configuration;

namespace WebService
{
    public class DependencyResolver : IDependencyResolver
    {
        private readonly Dictionary<Type, List<object>> keyValuePairs = new Dictionary<Type, List<object>>();
         private readonly string connectionString = new SettingProvider().GetValue("ConnectionString");

      //  private readonly string connectionString = ConfigurationManager.ConnectionStrings["DataBase"].ConnectionString;
        public IDependencyScope BeginScope()
        {
            return new DependencyResolver();
        }

        public void Dispose()
        {
            ;//нет реализации
        }

        public object GetService(Type serviceType)
        {
            if (serviceType == typeof(UsersController))
            {
                var controller = new UsersController(connectionString);
                AddInstance(serviceType, controller);
                return controller;
            }

            else if (serviceType == typeof(TurnoverTypesController))
            {
                var controller = new TurnoverTypesController(connectionString);
                AddInstance(serviceType, controller);
                return controller;
            }
            else if (serviceType == typeof(SourceTypesController))
            {
                var controller = new SourceTypesController(connectionString);
                AddInstance(serviceType, controller);
                return controller;
            }
            else if (serviceType == typeof(RecesiveTypesController))
            {
                var controller = new RecesiveTypesController(connectionString);
                AddInstance(serviceType, controller);
                return controller;
            }
            else if (serviceType == typeof(NdsValuesController))
            {
                var controller = new NdsValuesController(connectionString);
                AddInstance(serviceType, controller);
                return controller;
            }
            else if (serviceType == typeof(NaklStatusesController))
            {
                var controller = new NaklStatusesController(connectionString);
                AddInstance(serviceType, controller);
                return controller;
            }
            else if (serviceType == typeof(ContractTypesController))
            {
                var controller = new ContractTypesController(connectionString);
                AddInstance(serviceType, controller);
                return controller;
            }
            else if (serviceType == typeof(CompaniesController))
            {
                var controller = new CompaniesController(connectionString);
                AddInstance(serviceType, controller);
                return controller;
            }
            else if (serviceType == typeof(NaklsController))
            {
                var controller = new NaklsController(connectionString);
                AddInstance(serviceType, controller);
                return controller;
            }
            else if (serviceType == typeof(NaklFullsController))
            {
                var controller = new NaklFullsController(connectionString);
                AddInstance(serviceType, controller);
                return controller;
            }
            else if (serviceType == typeof(NaklItemsController))
            {
                var controller = new NaklItemsController(connectionString);
                AddInstance(serviceType, controller);
                return controller;
            }
            else if (serviceType == typeof(SgtinsController))
            {
                var controller = new SgtinsController(connectionString);
                AddInstance(serviceType, controller);
                return controller;
            }
            else if (serviceType == typeof(AcceptTypesController))
            {
                var controller = new AcceptTypesController(connectionString);
                AddInstance(serviceType, controller);
                return controller;
            }
            else
            {
                return null;
            }
        }

        private void AddInstance(Type serviceType, object controller)
        {
            if (keyValuePairs.ContainsKey(serviceType) == false)
            {
                keyValuePairs.Add(serviceType, new List<object>());
            }

            keyValuePairs[serviceType].Add(controller);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            List<object> value;

            if (keyValuePairs.TryGetValue(serviceType, out value) == true)
            {
                return value;
            }
            return new List<object>();
        }
    }
}
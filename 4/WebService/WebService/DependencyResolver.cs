using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using WebService.Controllers;
using WebService.DataBase.Address;
using WebService.DataBase.Company;
using WebService.DataBase.ContractType;
using WebService.DataBase.Nakl;
using WebService.DataBase.NaklItem;
using WebService.DataBase.NaklStatus;
using WebService.DataBase.RecesiverType;
using WebService.DataBase.Sgtin;
using WebService.DataBase.SourceType;
using WebService.DataBase.User;

namespace WebService
{
    public class DependencyResolver : IDependencyResolver
    {
        string connectionString = "Data Source=DESKTOP\\SQLEXPRESS;DataBase=g82; User ID=sa;Password=197312;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private readonly Dictionary<Type, List<object>> keyValuePairs = new Dictionary<Type, List<object>>();

        public DependencyResolver()
        {
            //  connectionString = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
        }

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
                var dataBase = new UserDataBase(connectionString);
                var controller = new UsersController(dataBase);
                AddInstance(serviceType, controller);
                return controller;

            }
            else if (serviceType == typeof(NaklStatusesController))
            {
                var dataBase = new NaklStatusDataBase(connectionString);
                var controller = new NaklStatusesController(dataBase);
                AddInstance(serviceType, controller);
                return controller;
            }
            else if (serviceType == typeof(SgtinsController))
            {
                var sgtinDataBase = new SgtinDataBase(connectionString);
                var naklDataBase = new NaklDataBase(connectionString);
                var naklItemDateBase = new NaklItemDataBase(connectionString);
                var controller = new SgtinsController(sgtinDataBase, naklDataBase, naklItemDateBase);
                AddInstance(serviceType, controller);
                return controller;
            }
            else if (serviceType == typeof(AddressesController))
            {
                var dataBase = new AddressDataBase(connectionString);
                var controller = new AddressesController(dataBase);
                AddInstance(serviceType, controller);
                return controller;
            }
            else if (serviceType == typeof(NaklsController))
            {
                var naklDataBase = new NaklDataBase(connectionString);
                var naklItemDataBase = new NaklItemDataBase(connectionString);
                var controller = new NaklsController(naklDataBase, naklItemDataBase);
                AddInstance(serviceType, controller);
                return controller;
            }
            else if (serviceType == typeof(NaklItemsController))
            {
                var dataBase = new NaklItemDataBase(connectionString);
                var controler = new NaklItemsController(dataBase);
                AddInstance(serviceType, controler);
                return controler;
            }
            else if (serviceType == typeof(CompaniesController))
            {
                var dataBase = new CompanyDataBase(connectionString);
                var controller = new CompaniesController(dataBase);
                AddInstance(serviceType, controller);
                return controller;
            }
            else if (serviceType == typeof(ReceiveTypesController))
            {
                var dataBase = new RecesiverTypeDataBase(connectionString);
                var controller = new ReceiveTypesController(dataBase);
                AddInstance(serviceType, controller);
                return controller;
            }
            else if (serviceType == typeof(ContractTypesController))
            {
                var dataBase = new ContractTypeDataBase(connectionString);
                var controller = new ContractTypesController(dataBase);
                AddInstance(serviceType, controller);
                return controller;
            }
            else if (serviceType == typeof(SourceTypesController))
            {
                var dataBase = new SourceTypDataBase(connectionString);
                var controller = new SourceTypesController(dataBase);
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
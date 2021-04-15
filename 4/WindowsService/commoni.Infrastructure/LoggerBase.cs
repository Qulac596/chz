using chz.WindowsServices.Setting;
using System;

namespace commoni.Infrastructure
{
    public abstract class LoggerBase
    {
        private string folderName;

        protected LoggerBase(string folderName, string serviceName, ISettingProvider setting, IDataBase dataBase)
        {
            this.folderName = folderName;
            Setting = setting;
            ServiceName = serviceName;
            DataBase = dataBase;
        }

        protected ISettingProvider Setting { get; private set; }

        protected string ServiceName { get; private set; }

        protected IDataBase DataBase { get; private set; }

        protected string GetPatch(DateTime dateTime)
        {
            var patch = folderName + "/" + dateTime.Year + "/" + dateTime.Month + "/" + dateTime.Day + "/";
            return patch;
        }
    }
}

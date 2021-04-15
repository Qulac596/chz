using commoni.Setting;
using Microsoft.Owin.Hosting;
using System;
using System.IO;
using System.ServiceProcess;

namespace WinService
{
    public partial class Service : ServiceBase
    {
        private IDisposable webApp;

        public Service()
        {
            InitializeComponent();
        }

        public void Start()
        {
            OnStart(null);
        }

        public void Stop()
        {
            OnStop();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                string baseAddress = new SettingProvider().GetValue("BaseUrl");

                webApp = WebApp.Start<Startup>(url: baseAddress);
            }
            catch (Exception e)
            {
                File.WriteAllText("C:/log.txt", e.Message);
            }
        }

        protected override void OnStop()
        {
            try
            {
                webApp.Dispose();
            }
            catch (Exception e)
            {
                File.WriteAllText("C:/log.txt", e.Message);
            }
        }
    }
}

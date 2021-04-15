using chz.WindowsServices.Log;
using chz.WindowsServices.Workes;

namespace chz.WindowsServices.UnloadDocument
{
    public partial class Service : MDLPService
    {
        private IWorker mDLPClientAdapter, signaller, canceler, unloader, statusInspector, linkLoader, ticketLoader, errorManager;

        public Service() : base()
        {
            InitializeComponent();
        }

        public Service(ILogger log, IWorker mDLPClientAdapter, IWorker signaller, IWorker canceler, IWorker unloader,
            IWorker statusInspector, IWorker linkLoader, IWorker ticketLoader, IWorker errorManager) : base(log)
        {
            InitializeComponent();

            this.mDLPClientAdapter = mDLPClientAdapter;
            this.signaller = signaller;
            this.canceler = canceler;
            this.unloader = unloader;
            this.statusInspector = statusInspector;
            this.linkLoader = linkLoader;
            this.ticketLoader = ticketLoader;
            this.errorManager = errorManager;
        }

        protected override void StartMDLPService()
        {
            mDLPClientAdapter.Start();
            errorManager.Start();
            canceler.Start();
            ticketLoader.Start();
            linkLoader.Start();
            statusInspector.Start();
            unloader.Start();
            signaller.Start();
        }

        protected override void StopMDLPService()
        {
            signaller.Stop();
            unloader.Stop();
            statusInspector.Stop();
            linkLoader.Stop();
            ticketLoader.Stop();
            canceler.Stop();
            errorManager.Stop();
            mDLPClientAdapter.Stop();
        }
        public void StartService()
        {
            OnStart(null);
        }

        public void StopService()
        {
            OnStop();
        }

        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            this.ServiceName = "Служба отправки документов в МДЛП.";
        }

        #endregion
    }
}

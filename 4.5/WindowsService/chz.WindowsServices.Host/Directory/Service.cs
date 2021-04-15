using chz.WindowsServices.Log;
using chz.WindowsServices.Workes;

namespace chz.WindowsServices.DirectoryLoader
{
    public partial class Service : MDLPService
    {
        private IWorker mDLPClientAdapter, signaller, branchLoader, errorManager;

        public Service() : base()
        {
            InitializeComponent();
        }

        public Service(ILogger log, IWorker mDLPClientAdapter, IWorker signaller, IWorker branchLoader, IWorker errorManager) : base(log)
        {
            InitializeComponent();

            this.mDLPClientAdapter = mDLPClientAdapter;
            this.signaller = signaller;
            this.branchLoader = branchLoader;
            this.errorManager = errorManager;
        }

        protected override void StartMDLPService()
        {
            mDLPClientAdapter.Start();
            errorManager.Start();
            branchLoader.Start();
            signaller.Start();
        }

        protected override void StopMDLPService()
        {
            signaller.Stop();
            branchLoader.Stop();
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
            this.ServiceName = "Служба загрузки справочников из МДЛП.";
        }
        #endregion
    }
}

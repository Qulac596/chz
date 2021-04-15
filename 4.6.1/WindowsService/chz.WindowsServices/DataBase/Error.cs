namespace chz.WindowsServices.DataBase
{
    /*
     * Базовый класс для ошибок работы с сервисом МДЛП
     */
    public abstract class Error
    {
        /*
         * Описание ошибки
         */
        public string Message { get; private set; }

        protected Error(string message)
        {
            Message = message;
        }
    }
}

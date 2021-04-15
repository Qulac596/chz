namespace chz.WindowsServices.DataBase
{
    /*
     * Ошибка не связанная с работой сервиса МДЛП
     */
    public class ServiceError : Error
    {
        public ServiceError(string message, ServiceErrorType serviceErrorType) : base(message)
        {
            ServiceErrorType = serviceErrorType;
        }

        /*
         * Тип ошибки службы
         */
        public ServiceErrorType ServiceErrorType { get; private set; }
    }
}

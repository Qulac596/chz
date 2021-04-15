namespace chz.WindowsServices.DataBase
{
    /*
     * Тип ошибки службы
     */
    public enum ServiceErrorType
    {
        /*
         * Не удалось выполнить запрос из-за ключевой проблемы, например подключения к сети, ошибки DNS, 
         * проверки сертификата сервера или времени ожидания.
         */
        HttpRequestError,

        /*
         * Ошибка при работе с криптографией
         */
        CryptographyError,
    }
}

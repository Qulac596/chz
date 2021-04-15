namespace chz.WindowsServices.UnloadDocument.DataBase
{
    /*
     * Статусы документа в службе
     */
    public enum DocumentServiceStatus
    {
        /*
         * Новый
         */
        New,

        /*
         * Загружен в МДЛП
         */
        UnLoad,

        /*
         * Обработан в МДЛП
         */
        Processed,

        /*
         * Ссылка загружена
         */
        LoadLink,

        /*
         * Тикет загружен
         */
        LoadTicket,
    }
}

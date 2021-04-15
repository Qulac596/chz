namespace chz.WindowsServices.ThisLoadDocument.DataBase
{
    /*
     * Статус документа в службе
     */
    public enum ServiceIncomeDocumentStatus
    {
        /*
         * Новый
         */
        New,

        /*
         * Ссылка загружена
         */
        LinkLoad,

        /*
         * Документ загружен
         */
        DocLoad,

        /*
         * Адрес загружен
         */
        AddressLoad
    }
}

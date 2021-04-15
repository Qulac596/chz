namespace chz.WindowsServices.Setting
{
    /*
     * Предоставляет доступ к настройкам
     */
    public interface ISettingProvider
    {
        /*
         * Возвращает значение по ключу
         */
        string GetValue(string key);
    }
}

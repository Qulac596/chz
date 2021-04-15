namespace chz.WindowsServices.Cryptography
{
    /*
     * Подписывает данные
     */
    public interface ISigner
    {
        /*
         * Подписывает строку
         */
        string Sign(string code);
    }
}
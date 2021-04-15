namespace chz.WindowsServices.Workes
{
    /*
     * Интерфейс активного объекта службы
     */
    public interface IWorker
    {
        /*
         * Вызывается при старте службы
         */
        void Start();

        /*
         * Вызывается при остановке службы
         */
        void Stop();
    }
}

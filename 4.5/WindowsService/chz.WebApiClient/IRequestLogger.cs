using chz.Client.WebApiClient;

namespace chz.WebApiClient
{
    /*
     * Пишел в лог дампы запросов к мдлп
     */
    public interface IRequestLogger
    {
        void Write(Request request);
    }
}

namespace commoni.Infrastructure
{
    public interface IDataBase
    {
        void Write(string serviceName,string msgType, string message);
    }
}

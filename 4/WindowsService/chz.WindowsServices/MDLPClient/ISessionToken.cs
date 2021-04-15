namespace chz.WindowsServices.MDLPClient
{
    public interface ISessionToken
    {
        int LifeTime { get;}

        string Token { get;}
    }
}
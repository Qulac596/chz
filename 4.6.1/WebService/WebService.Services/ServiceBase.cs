namespace WebService.Services
{
    public abstract class ServiceBase
    {
        protected string ConnectionStringPattern { get; private set; }

        protected string Login { get; private set; }

        protected string Password { get; private set; }

        protected ServiceBase() { }

        protected ServiceBase(string connectionStringPattern, string login, string password)
        {
            ConnectionStringPattern = connectionStringPattern;
            Login = login;
            Password = password;
        }
    }
}

using System;
using System.Threading.Tasks;

namespace chz.WindowsServices.MDLPClient
{
    public interface IMDLPClient : IDisposable
    {
        string AccesToken { set; }

        string BaseUrl { set; }

        Task Logout();

        Task<IAuntificationCode> GetCode(AuntificationUserInfo auntificationUserInfo);

        Task<ISessionToken> GetSessionToken(AuthenticatedUserInfo authenticatedUserInfo);

        Task<string> LoadStringContent(string url, string targetId=null);
    }
}

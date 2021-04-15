using System.Net.Http;
using System.Threading.Tasks;

namespace chz.WindowsServices.DirectoryLoader.MDLPClient
{
    public interface IMDLPClient: WindowsServices.MDLPClient.IMDLPClient
    {
        Task<string> SendRequest(string targetId, HttpMethod httpMethod, string url, string json);
    }
}

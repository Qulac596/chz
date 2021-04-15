using chz.Client.WebApiClient;
using chz.WindowsServices.MDLPClient;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace chz.Client.DirectoryLoader
{
    public class MDLPClient : Client.MDLPClient, WindowsServices.DirectoryLoader.MDLPClient.IMDLPClient
    {

        public MDLPClient(IClient client, string url) : base(client, url) { }

        public async Task<string> SendRequest(string targetId, HttpMethod httpMethod, string url, string json)
        {
            var request = new Request(httpMethod, new Uri(BaseUrl + url), json, targetId: targetId, accessToken: AccesToken);

            await Client.Send(request);

            if (request.Response.HttpStatusCode == HttpStatusCode.OK)
            {
                return request.Response.ResponseString;
            }

            throw new MDLPClientException(MDLPErrorMessage, request.Response.HttpStatusCode, request.Response.ResponseString);
        }
    }
}

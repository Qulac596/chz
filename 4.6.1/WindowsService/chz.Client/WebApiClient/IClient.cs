using System;
using System.Threading.Tasks;

namespace chz.Client.WebApiClient
{
    public interface IClient : IDisposable
    {
        Task Send(Request request);
    }
}
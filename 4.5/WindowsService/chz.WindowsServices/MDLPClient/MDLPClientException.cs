using System;
using System.Net;

namespace chz.WindowsServices.MDLPClient
{
    public class MDLPClientException : Common.CZException
    {
        public MDLPClientException(string message, HttpStatusCode httpStatusCode, string response) : base(message)
        {
            Guid = new Guid();
            HttpStatusCode = httpStatusCode;
            Response = response;
        }

        public Guid Guid { get; private set; }

        public HttpStatusCode HttpStatusCode { get; private set; }

        public string Response { get; set; }
    }
}

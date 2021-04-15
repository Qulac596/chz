using System;
using System.Net;

namespace chz.Client.WebApiClient
{
    public class Response
    {
        internal Response(DateTime dateTime, HttpStatusCode httpStatusCode, string responseString)
        {
            DateTime = dateTime;
            HttpStatusCode = httpStatusCode;
            ResponseString = responseString;
        }

        public DateTime DateTime { get; set; }

        public HttpStatusCode HttpStatusCode { get; set; }

        public string ResponseString { get; set; }
    }
}

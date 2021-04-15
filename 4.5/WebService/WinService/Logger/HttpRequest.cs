using System;

namespace WinService.Logger
{
    public class HttpRequest
    {
        public DateTime DateTime { get; set; }

        public string Host { get; set; }

        public string Method { get; set; }

        public string Url { get; set; }

        public string Body { get; set; }

        public string QueryString { get; set; }

        public string Login { get; set; }

    }
}

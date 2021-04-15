using System;
using Tools.SqlTransact;

namespace WinService.Logger
{
    public class InsertParameter
    {
        [ParamName("request_data")]
        public DateTime ReguestDateTime { get; set; }

        [ParamName("request_host")]
        public string Host { get; set; }

        [ParamName("request_method")]
        public string Method { get; set; }

        [ParamName("request_url")]
        public string Url { get; set; }

        [ParamName("request_body")]
        public string Body { get; set; }

        [ParamName("request_query_string")]
        public string QueryString { get; set; }

        [ParamName("response_status_code")]
        public int StatusCode { get; set; }

        [ParamName("response_result")]
        public string Result { get; set; }

        [ParamName("login")]
        public string Login { get; set; }
    }
}

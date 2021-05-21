using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WS_Ecommerce.BataClub
{
    public class VerifyEmailJsonResult
    {
        public Data Data { get; set; }
        public string Message { get; set; }
        public string MessageDetail { get; set; }
        public Int32 StatusCode { get; set; }
    }
    public class Data
    {
        public string Response { get; set; }
        public Response_obj Response_obj { get; set; }

    }
    public class objeto_json
    {
        public VerifyEmailJsonResult VerifyEmailJsonResult { get; set; }       

    }
    public class Response_obj
    {
        public string result { get; set; }
        public string reason { get; set; }
        public string role { get; set; }
        public string free { get; set; }
        public string disposable { get; set; }
        public string accept_all { get; set; }
        public string did_you_mean { get; set; }
        public string sendex { get; set; }
        public string email { get; set; }
        public string user { get; set; }
        public string domain { get; set; }
        public string success { get; set; }
        public string message { get; set; }
    }
}
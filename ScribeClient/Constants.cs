using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace ScribeClient
{
    public class Constants
    {
        //Server Level Constants
        public const string BASE_URI_API = "http://localhost:5001";
        public const string BASE_URI_AUTH = "http://localhost:59060";
        public const string AUTH_SECRET = "SuperSecretKey";
        public const string API_NAME = "note_api";
        public const string AUTH_CLIENT = "ro.client";

        public static string AccessToken { get; internal set; }
    }
}

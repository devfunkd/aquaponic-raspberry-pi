using System;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Web;

namespace AquaCultureMonitor.Core.Utilities
{
    [ExcludeFromCodeCoverage]
    public static class ApplicationPath
    {
        public static string GetSiteRoot()
        {
            string port = HttpContext.Current.Request.ServerVariables["SERVER_PORT"];
            if (port == null || port == "80" || port == "443")
                port = "";
            else
                port = ":" + port;

            string protocol = HttpContext.Current.Request.ServerVariables["SERVER_PORT_SECURE"];
            if (protocol == null || protocol == "0")
                protocol = "http://";
            else
                protocol = "https://";

            string sOut = protocol + HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + port +
                          HttpContext.Current.Request.ApplicationPath;

            if (sOut.EndsWith("/"))
            {
                sOut = sOut.Substring(0, sOut.Length - 1);
            }

            return sOut;
        }
    }
}
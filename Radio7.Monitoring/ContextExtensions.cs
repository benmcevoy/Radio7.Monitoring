using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Radio7.Monitoring
{
    public static class ContextExtensions
    {
        public static string GetUrl(this IDictionary<string, object> value)
        {
            return value["url"] as string;
        }

        public static string GetPath(this IDictionary<string, object> value)
        {
            return value["path"] as string;
        }

        public static Site GetSite(this IDictionary<string, object> value)
        {
            return value["site"] as Site;
        }

        public static HttpStatusCode GetStatusCode(this IDictionary<string, object> value)
        {
            return (HttpStatusCode)value["statusCode"];
        }

        public static long GetResponseTimeinMilliSeconds(this IDictionary<string, object> value)
        {
            return (long)value["responseTimeinMilliSeconds"];
        }

        public static string GetResponseRaw(this IDictionary<string, object> value)
        {
            return value["responseRaw"] as string;
        }

        public static IList<string> GetErrors(this IDictionary<string, object> value)
        {
            return value["errors"] as IList<string>;
        }

        public static void SetUrl(this IDictionary<string, object> value, string url)
        {
            value["url"] = url;
        }

        public static void SetPath(this IDictionary<string, object> value, string path)
        {
            value["path"] = path;
        }
        public static void SetSite(this IDictionary<string, object> value, Site site)
        {
            value["site"] = site;
        }

        public static void SetStatusCode(this IDictionary<string, object> value, HttpStatusCode statusCode)
        {
            value["statusCode"] = statusCode;
        }

        public static void SetResponseTimeinMilliSeconds(this IDictionary<string, object> value, long responseTimeinMilliSeconds)
        {
            value["responseTimeinMilliSeconds"] = responseTimeinMilliSeconds;
        }

        public static void SetResponseRaw(this IDictionary<string, object> value, string responseRaw)
        {
            value["responseRaw"] = responseRaw;
        }

        public static void SetError(this IDictionary<string, object> value, string error)
        {
            var errors = value.GetErrors();
            var sb = new StringBuilder();

            sb.AppendFormat("error: '{0}'{1}", error, Environment.NewLine);

            foreach (var key in value.Keys)
            {
                if (key == "responseRaw") continue;
                if (key == "errors") continue;

                sb.AppendFormat("{0}: '{1}'{2}", key, value[key], Environment.NewLine);
            }

            errors.Add(sb.ToString());

            value["errors"] = errors;
        }

        public static void SetErrors(this IDictionary<string, object> value, IList<string> errors)
        {
            value["errors"] = errors;
        }
    }
}

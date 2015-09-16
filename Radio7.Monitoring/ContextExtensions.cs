using System.Collections.Generic;
using System.Net;

namespace Radio7.Monitoring
{
    public static class ContextExtensions
    {
        public static string GetUrl(this IDictionary<string, object> value)
        {
            return value["url"] as string;
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

        public static void SetErrors(this IDictionary<string, object> value, string error)
        {
            var errors = value.GetErrors();
            errors.Add(error);
            value["errors"] = errors;
        }
    }
}

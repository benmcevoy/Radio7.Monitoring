using System;

namespace Radio7.Monitoring
{
    class UrlHelper
    {
        public static string ToAbsoluteUrl(string absoluteOrRelativeUrl, string baseUrl)
        {
            var url = new Uri(absoluteOrRelativeUrl, UriKind.RelativeOrAbsolute);

           return url.IsAbsoluteUri
                ? url.ToString()
                : new Uri(new Uri(baseUrl, UriKind.Absolute), absoluteOrRelativeUrl).ToString();
        }
    }
}

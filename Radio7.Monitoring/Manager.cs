using Radio7.Monitoring.Pipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Radio7.Monitoring
{
    public class Manager
    {

        public Manager()
        {

        }


        public void Run(SiteCollection sites)
        {
            foreach (var site in sites.Sites)
            {
                using (var wc = new HttpClient())
                {
                    foreach (var path in site.Paths)
                    {
                        var sw = System.Diagnostics.Stopwatch.StartNew();
                        var url = GetUrl(path, site.BaseUrl);
                        var response = wc.GetAsync(url).Result;

                        sw.Stop();

                        var context = CreateContext(url, sw.ElapsedMilliseconds, response);

                        foreach (var test in site.Tests)
                        {
                            context = ((IFilter)Activator.CreateInstance(Type.GetType(test))).Run(context);
                        }
                    }
                }
            }
        }

        private IDictionary<string, object> CreateContext(string url, long elapsed, HttpResponseMessage response)
        {
            var context = new Dictionary<string, object>();

            context.SetUrl(url);
            context.SetStatusCode(response.StatusCode);
            context.SetResponseRaw(response.Content.ToString());
            context.SetResponseTimeinMilliSeconds(elapsed);

            return context;
        }

        private string GetUrl(string path, string baseUrl)
        {
            var url = new Uri(path, UriKind.RelativeOrAbsolute);

            if (url.IsAbsoluteUri)
            {
                return url.ToString();
            }

            return new Uri(new Uri(baseUrl, UriKind.Absolute), path).ToString();
        }
    }
}

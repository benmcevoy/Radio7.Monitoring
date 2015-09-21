using System.Collections.Generic;
using System.Net.Http;

namespace Radio7.Monitoring.Filters
{
    class GetResponse : IFilter
    {
        public IDictionary<string, object> Run(IDictionary<string, object> context)
        {
            var site = context.GetSite();

            if (!site.EnforceSslCertificatateValidation)
            {
                using (new SslDisabler())
                {
                    DoRequest(context, site);
                    return context;
                }
            }

            DoRequest(context, site);
            
            return context;
        }

        private static void DoRequest(IDictionary<string, object> context, Site site)
        {
            using (var wc = new HttpClient())
            {
                if (site.DoWarmupRequest)
                {
                    var warmup = wc.GetAsync(context.GetUrl()).Result;
                }

                var sw = System.Diagnostics.Stopwatch.StartNew();
            
                var response = wc.GetAsync(context.GetUrl()).Result;
                var body = response.Content.ReadAsStringAsync().Result;

                sw.Stop();

                context.SetStatusCode(response.StatusCode);
                context.SetResponseRaw(body);
                context.SetResponseTimeinMilliSeconds(sw.ElapsedMilliseconds);
            }
        }
    }
}
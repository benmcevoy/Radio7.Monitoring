using System.Collections.Generic;
using System.Net.Http;
using Radio7.Monitoring.Pipes;

namespace Radio7.Monitoring.Filters
{
    class GetResponse : IFilter
    {
        public IDictionary<string, object> Run(IDictionary<string, object> context)
        {
            using (var wc = new HttpClient())
            {
                var sw = System.Diagnostics.Stopwatch.StartNew();
                var response = wc.GetAsync(context.GetUrl()).Result;
                var body = response.Content.ReadAsStringAsync().Result;
                
                sw.Stop();

                context.SetStatusCode(response.StatusCode);
                context.SetResponseRaw(body);
                context.SetResponseTimeinMilliSeconds(sw.ElapsedMilliseconds);
            }
            
            return context;
        }
    }
}
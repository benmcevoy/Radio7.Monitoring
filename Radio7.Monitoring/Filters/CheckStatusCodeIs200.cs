using System.Collections.Generic;
using System.Net;

namespace Radio7.Monitoring.Filters
{
    public class CheckStatusCodeIs200 : IFilter
    {
        public IDictionary<string, object> Run(IDictionary<string, object> context)
        {
            if (context.GetStatusCode() != HttpStatusCode.OK)
                context.SetError("Status code was not OK.");

            return context;
        }
    }
}

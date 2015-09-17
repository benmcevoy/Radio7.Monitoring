using Radio7.Monitoring.Pipes;
using System.Collections.Generic;
using System.Net;

namespace Radio7.Monitoring.Tests
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

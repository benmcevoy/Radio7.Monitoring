using System.Collections.Generic;
using Radio7.Monitoring.Pipes;

namespace Radio7.Monitoring.Tests
{
    public class CheckResponseTimeIsLessThan10Seconds : IFilter
    {
        public IDictionary<string, object> Run(IDictionary<string, object> context)
        {
            if (context.GetResponseTimeinMilliSeconds() > 10000) 
                context.SetError("Request took longer than 10 seconds to respond.");

            return context;
        }
    }
}

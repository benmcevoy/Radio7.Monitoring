using System.Collections.Generic;
using Radio7.Monitoring.Filters;
using Radio7.Monitoring;

namespace ConsoleHost.Tests
{
    public class CheckResponseTimeIsLessThanNSeconds : IFilter
    {
        private readonly int _seconds;
        private readonly int _milliSeconds;

        public CheckResponseTimeIsLessThanNSeconds(int seconds)
        {
            _seconds = seconds;
            _milliSeconds = seconds * 1000;
        }

        public IDictionary<string, object> Run(IDictionary<string, object> context)
        {
            if (context.GetResponseTimeinMilliSeconds() > _milliSeconds) 
                context.SetError("Request took longer than " + _seconds + " seconds to respond.");

            return context;
        }
    }
}

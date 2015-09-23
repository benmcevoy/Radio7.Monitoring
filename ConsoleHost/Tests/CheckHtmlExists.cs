using Radio7.Monitoring.Filters;
using Radio7.Monitoring;
using System.Collections.Generic;
using System.Globalization;
using culture = System.Globalization.CultureInfo;

namespace ConsoleHost.Tests
{
    class CheckHtmlExists : IFilter
    {
        private readonly string _html;

        public CheckHtmlExists(string html)
        {
            _html = html;
        }

        public IDictionary<string, object> Run(IDictionary<string, object> context)
        {
            if (culture.InvariantCulture.CompareInfo.IndexOf(context.GetResponseRaw(), _html, CompareOptions.OrdinalIgnoreCase) == 0)
                context.SetError("Response does not contain '" + _html + "'.");

            return context;
        }
    }

    class CheckHtmlDoesNotExist : IFilter
    {
        private readonly string _html;

        public CheckHtmlDoesNotExist(string html)
        {
            _html = html;
        }

        public IDictionary<string, object> Run(IDictionary<string, object> context)
        {

            if (culture.InvariantCulture.CompareInfo.IndexOf(context.GetResponseRaw(), _html, CompareOptions.OrdinalIgnoreCase) > 0)
                context.SetError("Response contains '" + _html + "'.");

            return context;
        }
    }
}

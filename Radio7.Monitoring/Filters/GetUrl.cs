using System;
using System.Collections.Generic;
using Radio7.Monitoring.Pipes;

namespace Radio7.Monitoring.Filters
{
    class GetUrl : IFilter
    {
        public IDictionary<string, object> Run(IDictionary<string, object> context)
        {
            var path = context.GetPath();
            var baseUrl = context.GetSite().BaseUrl;

            var url = new Uri(path, UriKind.RelativeOrAbsolute);

            var result = url.IsAbsoluteUri 
                ? url.ToString() 
                : new Uri(new Uri(baseUrl, UriKind.Absolute), path).ToString();

            context.SetUrl(result);

            return context;
        }
    }
}

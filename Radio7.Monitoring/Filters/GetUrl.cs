using System.Collections.Generic;

namespace Radio7.Monitoring.Filters
{
    class GetUrl : IFilter
    {
        public IDictionary<string, object> Run(IDictionary<string, object> context)
        {
            var path = context.GetPath();
            var baseUrl = context.GetSite().BaseUrl;
            var result = UrlHelper.ToAbsoluteUrl(path, baseUrl);

            context.SetUrl(result);

            return context;
        }
    }
}

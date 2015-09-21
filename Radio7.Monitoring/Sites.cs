using Radio7.Monitoring.Filters;
using System.Collections.Generic;
using System.Linq;

namespace Radio7.Monitoring
{
    public class SiteCollection
    {
        public IEnumerable<Site> Sites { get; set; }
    }

    public class Site
    {
        public Site()
        {
            Paths = Enumerable.Empty<string>();
            Tests = Enumerable.Empty<IFilter>();
        }

        public override string ToString()
        {
            return string.Format(@"name: '{0}'; Do warm up: '{1}'; Enforce Ssl: '{2}'; Base url: '{3}'; Site map url: '{4}'",
                Name, DoWarmupRequest, EnforceSslCertificatateValidation, BaseUrl, SiteMapUrl);
        }

        public string Name { get; set; }
        public bool DoWarmupRequest { get; set; }
        public string BaseUrl { get; set; }
        public string SiteMapUrl { get; set; }
        public string SitemapXmlns { get; set; }
        public bool EnforceSslCertificatateValidation { get; set; }
        public IEnumerable<string> Paths { get; set; }
        public IEnumerable<IFilter> Tests { get; set; }
    }
}

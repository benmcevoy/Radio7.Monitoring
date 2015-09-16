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
            Tests = Enumerable.Empty<string>();
        }

        public string BaseUrl { get; set; }
        public string SiteMapUrl { get; set; }
        public bool EnforceSSLCertificatateValidation { get; set; }
        public IEnumerable<string> Paths { get; set; }
        public IEnumerable<string> Tests { get; set; }
    }
}

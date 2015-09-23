using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Xml.Linq;

namespace Radio7.Monitoring
{
    public class SitemapProcessor
    {
        public IEnumerable<string> Process(Site site)
        {
            if (string.IsNullOrWhiteSpace(site.SiteMapUrl)) return Enumerable.Empty<string>();

            using (new SslDisabler(site.DisableSslCertificatateValidation))
            using (var wc = new WebClient())
            {
                var siteMapUrl = UrlHelper.ToAbsoluteUrl(site.SiteMapUrl, site.BaseUrl);
                var siteMap = wc.DownloadString(siteMapUrl);

                if (string.IsNullOrWhiteSpace(siteMap)) return Enumerable.Empty<string>();

                XNamespace xmlns = string.IsNullOrWhiteSpace(site.SitemapXmlns)
                    ? "http://www.sitemaps.org/schemas/sitemap/0.9"
                    : site.SitemapXmlns;

                var xml = XElement.Parse(siteMap);

                return xml.Descendants(xmlns + "loc").Select(element => element.Value.Trim());
            }
        }
    }
}

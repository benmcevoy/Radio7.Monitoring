using Radio7.Monitoring.Filters;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Radio7.Monitoring
{
    public class Manager
    {
        private readonly SitemapProcessor _sitemapProcessor;

        public Manager(SitemapProcessor sitemapProcessor)
        {
            _sitemapProcessor = sitemapProcessor;
        }

        public ICollection<string> Run(SiteCollection sites)
        {
            var errors = new List<string>(2048);

            foreach (var site in sites.Sites)
            {
                Debug.WriteLine("Began processing site:" + site.Name);

                var tests = site.Tests.ToList();
                var paths = _sitemapProcessor.Process(site).Union(site.Paths);

                foreach (var path in paths)
                {
                    Debug.WriteLine("Began processing path:" + path);

                    var ctx = CreateContext(site, path, errors);

                    new Pipeline(
                            new IFilter[] { new GetUrl(), new GetResponse() }.Union(tests))
                        .Run(ctx);

                    errors = ctx.GetErrors().ToList();
                }
            }

            return errors;
        }

        private static IDictionary<string, object> CreateContext(Site site, string path, IList<string> errors)
        {
            var context = new Dictionary<string, object>();
            context.SetErrors(errors);
            context.SetSite(site);
            context.SetPath(path);

            return context;
        }
    }
}

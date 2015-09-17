using Radio7.Monitoring.Pipes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Radio7.Monitoring.Filters;

namespace Radio7.Monitoring
{
    public class Manager
    {
        public IEnumerable<string> Run(SiteCollection sites)
        {
            var errors = new List<string>(2048);

            foreach (var site in sites.Sites)
            {
                Debug.WriteLine("Began processing site:" + site);

                var tests = site.Tests.Select(test => (IFilter)Activator.CreateInstance(Type.GetType(test))).ToList();
                var paths = site.Paths;

                foreach (var path in paths)
                {
                    Debug.WriteLine("Began processing path:" + path);

                    var ctx = CreateContext(site, path, errors);

                    Pipeline
                        .Create(
                            new IFilter[] { new GetUrl(), new GetResponse() }.Union(tests))
                        .Run(ctx);

                    errors = ctx.GetErrors().ToList();
                }
            }

            return errors;
        }

        //private IEnumerable<string> ProcessSitemap(Site site)
        //{
        //    if (string.IsNullOrWhiteSpace(site.SiteMapUrl)) return Enumerable.Empty<string>();

        //    using (var wc = new WebClient())
        //    {
        //        var siteMap = wc.DownloadString(site.SiteMapUrl);

        //        if (string.IsNullOrWhiteSpace(siteMap)) return Enumerable.Empty<string>();


        //    }
        //}

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

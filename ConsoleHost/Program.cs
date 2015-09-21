using Radio7.Monitoring;
using Radio7.Monitoring.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var sites = new SiteCollection();

            sites.Sites = new List<Site>()
            {
                new Site
                    {
                        Name = "my website",
                        DoWarmupRequest = true,
                        EnforceSslCertificatateValidation = false,
                        BaseUrl = "https://mywebsite.local/",
                        SiteMapUrl = "https://mywebsite.local/sitemap.xml",
                        Tests = new List<IFilter>
                        {
                            new CheckStatusCodeIs200(),
                            new Tests.CheckResponseTimeIsLessThanNSeconds(2)
                        }
                    },
            };

            var errors = new Manager(new SitemapProcessor()).Run(sites);

            foreach (var error in errors)
            {
                Console.WriteLine(error);
                Debug.WriteLine(error);
            }

            Console.WriteLine("done");
            Console.ReadKey();
        }
    }
}

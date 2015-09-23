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
                        Name = "Seniors",
                        DoWarmupRequest = true,
                        DisableSslCertificatateValidation = true,
                        BaseUrl = "https://www.seniorsonline.vic.gov.au/",
                        //SiteMapUrl = "sitemap.xml",
                        Paths = new []{ "https://www.seniorsonline.vic.gov.au/" },
                        Tests = new List<IFilter>
                        {
                            new CheckStatusCodeIs200(),
                            new Tests.CheckResponseTimeIsLessThanNSeconds(2),
                            new Tests.CheckHtmlExists(@"<div id=""layout"" "),
                            new Tests.CheckHtmlDoesNotExist(@"<h1>Error</h1>"),
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

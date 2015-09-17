using Radio7.Monitoring;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var sites = new SiteCollection();

            sites.Sites = new List<Site>()
            {
                new Site {
                    BaseUrl = "https://www.seniorsonline.vic.gov.au/",
                    Tests = new List<string>
                    {
                        "Radio7.Monitoring.Tests.CheckStatusCodeIs200, Radio7.Monitoring",
                        "Radio7.Monitoring.Tests.CheckResponseTimeIsLessThan10Seconds, Radio7.Monitoring"
                    },
                    Paths = new List<string>
                    {
                        "https://www.seniorsonline.vic.gov.au/festivalsandawards/past-highlights/festival programs",
                        "https://www.seniorsonline.vic.gov.au/news-opinions/blogs/blog-topics/balanced-life",
                        "https://www.seniorsonline.vic.gov.au/news-opinions/blogs/blog-topics/being-with-our-elders",

                    }
                }
            };

            var errors = new Manager().Run(sites);

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

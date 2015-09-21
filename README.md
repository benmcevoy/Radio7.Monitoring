# Radio7.Monitoring
push a bunch of URL's through a pipeline

pipelines came from here: https://github.com/benmcevoy/Pipelines

rough sketch of a monitoring service

give it a list of url's or (eventually) a sitemap.xml

then push the response of each through a pipeline

accumulate any errors and whatnot

and then do something, like fail a test or make a report

definition of a site and set of urls to check looks like:

i'm using the wonderful seniorsonline cos that's the last site that went live

```
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
```  

and internally the pipeline looks like

```
  var ctx = CreateContext(site, path, errors);

  Pipeline
    .Create(
        new IFilter[] { new GetUrl(), new GetResponse() }.Union(tests))
    .Run(ctx);

```

Where `GetUrl`, `GetResponse` and the union of `tests` are all "filters" in the pipe.
Each url is pushed through this pipeline and accumulates any errors or what have you

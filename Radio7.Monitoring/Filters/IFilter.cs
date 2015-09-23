using System.Collections.Generic;

namespace Radio7.Monitoring.Filters
{
    public interface IFilter
    {
        IDictionary<string, object> Run(IDictionary<string, object> context);
    }
}
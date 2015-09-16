using System.Collections.Generic;

namespace Radio7.Monitoring.Pipes
{
    public interface IFilter
    {
        IDictionary<string, object> Run(IDictionary<string, object> context);
    }
}
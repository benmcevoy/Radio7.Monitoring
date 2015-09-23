using System.Collections.Generic;
using System.Linq;

namespace Radio7.Monitoring.Filters
{
    public class Pipeline : IFilter
    {
        private readonly IEnumerable<IFilter> _filters;

        public Pipeline(IEnumerable<IFilter> filters)
        {
            _filters = filters;
        }

        public IDictionary<string, object> Run(IDictionary<string, object> context)
        {
            return _filters.Aggregate(context, (current, filter) => filter.Run(current));
        }
    }
}

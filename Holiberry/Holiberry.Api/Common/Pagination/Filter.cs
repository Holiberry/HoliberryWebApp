using System.Collections.Generic;

namespace Holiberry.Api.Common.Pagination
{
    public class Filter
    {
        public string Field { get; set; }
        public string Operator { get; set; }
        public object Value { get; set; }
        public string Logic { get; set; }
        public List<Filter> Filters { get; set; } = new List<Filter>();
    }
}

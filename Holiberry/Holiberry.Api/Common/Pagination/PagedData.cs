namespace Holiberry.Api.Common.Pagination
{
    public class PagedData
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public int? ResultsCount { get; set; }
        public int? Pages { get; set; }
    }
}

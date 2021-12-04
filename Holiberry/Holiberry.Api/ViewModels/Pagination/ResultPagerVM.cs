using System.Linq;

namespace Holiberry.Api.ViewModels.Pagination
{
    public class ResultPager
    {
        public ResultPager()
        {
            CurrentPage = 1;
        }

        public int CurrentPage { get; set; }
        public int Pages { get; set; }
        public int ResultsPerPage { get; set; }

        public int ResultsCount { get; set; }

        public void SetCurrentPage(int? currentPage)
        {
            if (currentPage.HasValue && currentPage > 0)
            {
                CurrentPage = currentPage.Value - 1;
            }
            else
            {
                CurrentPage = 0;
            }
        }

        public void SetParams(int? page, int? perPage)
        {
            SetCurrentPage(page);
            SetResultsPerPage(perPage);
        }

        public void SetResultsPerPage(int? perPage)
        {
            ResultsPerPage = perPage.HasValue ? perPage.Value : AvailablePageSizes()[0];
        }

        public static int[] AvailablePageSizes()
        {
            return new int[] { 25, 50, 100 };
        }

        public int SkipRecords => CurrentPage * ResultsPerPage;

        public bool HasPrevPage { get; set; } = false;
        public bool HasNextPage { get; set; } = false;

        public static IQueryable<T> PaginatedQuery<T>(IQueryable<T> entities, ResultPager info) where T : class
        {
            IQueryable<T> resultCollection = entities
                .Skip(info.SkipRecords)
                .Take(info.ResultsPerPage);

            //Get Entities Count
            info.ResultsCount = entities.Count();

            double pages = info.ResultsCount / (double)info.ResultsPerPage;
            if (double.IsInfinity(pages))
            {
                info.Pages = 0;
            }
            else
            {
                info.Pages = (int)pages;
                if (pages - (int)pages != 0)
                {
                    info.Pages += 1;
                }
            }

            //Check if pager has prev page
            info.HasPrevPage = info.CurrentPage > 0;

            //Check if pager has next page
            info.HasNextPage = info.CurrentPage < info.Pages - 1;

            return resultCollection;
        }
    }
}
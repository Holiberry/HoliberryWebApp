using Holiberry.Api.Common.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holiberry.Api.ViewModels.Pagination
{
    public abstract class PagerVM
    {
        public string SortField { get; set; } = "Id";
        public string SortDir { get; set; } = "DESC";
        public int CurrentPage { get; set; } = 1;
        public int PerPage { get; set; } = 25;

        public PaginationFilter PaginationFilter => new PaginationFilter(CurrentPage, PerPage, SortField, SortDir);
    }
}

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Holiberry.Api.Common.Pagination
{
    public class PaginationFilter
    {
        protected string _sortField;
        public string SortField
        {
            get => _sortField;
            set => _sortField = value;
        }

        protected string _sortDir;
        public string SortDir
        {
            get => _sortDir;
            set => _sortDir = value;
        }

        //public List<Sort> Sort { get; set; } = new List<Sort>();
        //public Filter Filter { get; set; }
        public List<FilterItem> Filters { get; set; } = new List<FilterItem>();

        private int _currentPage;
        public int CurrentPage
        {
            get => _currentPage;
            set => _currentPage = value > 0 ? value : 1;
        }

        private int _perPage;
        public int PerPage
        {
            get => _perPage;
            set => _perPage = value > 0 ? value : 25;
        }

        public PaginationFilter()
        {
            _currentPage = 1;
            _perPage = 25;
        }

        public PaginationFilter(int currentPage, int perPage)
        {
            CurrentPage = currentPage;
            PerPage = perPage;
        }

        public PaginationFilter(int currentPage, int perPage, string sortField, string sortDir)
        {
            _currentPage = currentPage;
            _perPage = perPage;
            SortField = sortField;
            SortDir = sortDir;
        }

        public PaginationFilter(int currentPage, int perPage, string sortField, string sortDir, List<FilterItem> filters)
        {
            _currentPage = currentPage;
            _perPage = perPage;
            SortField = sortField;
            SortDir = sortDir;
            Filters = filters;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Holiberry.Api.Common.Pagination.Extensions
{
    public static class QueryableExtensions
    {
        /// <summary>
        /// Sortuje po podanym polu
        /// </summary>
        public static IQueryable<T> ToFilterView<T>(this IQueryable<T> query, PaginationFilter filter)
        {
            // filter
            //query = Filter(query, filter.Filter);
            //sort
            if (filter.SortField != null && filter.SortDir != null)
            {
                if (!string.Equals(filter.SortDir, "DESC", StringComparison.CurrentCultureIgnoreCase) && !string.Equals(filter.SortDir, "ASC", StringComparison.CurrentCultureIgnoreCase))
                    filter.SortDir = "ASC";

                List<Sort> sortList = new List<Sort>();
                if (string.IsNullOrEmpty(filter.SortDir) && string.IsNullOrEmpty(filter.SortField))
                {
                    var defaultsort = new Sort() { Field = "Id", Dir = "DESC" };
                    sortList.Add(defaultsort);
                }
                else
                {
                    var defaultsort = new Sort() { Field = filter.SortField, Dir = filter.SortDir };
                    sortList.Add(defaultsort);
                }
                query = Sort(query, filter.SortField, filter.SortDir);
                // EF does not apply skip and take without order
                //query = Limit(query, filter.PageSize, filter.PageNumber);
            }
            else
            {
                var defaultsort = new Sort() { Field = "Id", Dir = "DESC" };
                List<Sort> sortList = new List<Sort> { defaultsort };
                query = Sort(query, filter.SortField, filter.SortDir);
            }
            return query;
        }

        public static IQueryable<T> ToPaginationView<T>(this IQueryable<T> query, PaginationFilter filter)
        {
            // EF does not apply skip and take without order
            query = Limit(query, filter.PerPage, filter.CurrentPage);
            return query;
        }

        private static IQueryable<T> Filter<T>(IQueryable<T> queryable, Filter filter)
        {
            if (filter != null && filter.Filters.Any())
            {
                var filters = GetAllFilters(filter);
                var values = filters.Select(f => f.Value).ToArray();
                var where = Transform(filter, filters);
                queryable = queryable.Where(where, values);
            }
            return queryable;
        }

        private static IQueryable<T> Sort<T>(IQueryable<T> queryable, string sortField, string sortDir)
        {
            if (!string.IsNullOrEmpty(sortField) && !string.IsNullOrEmpty(sortDir))
            {
                var ordering = sortField + " " + sortDir;
                queryable = queryable.OrderBy(ordering);
            }
            else
            {
                var ordering = "Id" + " " + "DESC";
                queryable = queryable.OrderBy(ordering);
            }
            return queryable;
        }

        private static IQueryable<T> Limit<T>(IQueryable<T> queryable, int limit, int offset)
        {
            int skipRecords = (offset - 1) * limit;
            return queryable.Skip(skipRecords).Take(limit);
        }

        private static readonly IDictionary<string, string>
        Operators = new Dictionary<string, string>
        {
            {"eq", "="},
            {"neq", "!="},
            {"lt", "<"},
            {"lte", "<="},
            {"gt", ">"},
            {"gte", ">="},
            {"startswith", "StartsWith"},
            {"endswith", "EndsWith"},
            {"contains", "Contains"},
            {"doesnotcontain", "Contains"},
        };

        public static IList<Filter> GetAllFilters(Filter filter)
        {
            var filters = new List<Filter>();
            GetFilters(filter, filters);
            return filters;
        }

        private static void GetFilters(Filter filter, IList<Filter> filters)
        {
            if (filter.Filters != null && filter.Filters.Any())
            {
                foreach (var item in filter.Filters)
                {
                    GetFilters(item, filters);
                }
            }
            else
            {
                filters.Add(filter);
            }
        }

        public static string Transform(Filter filter, IList<Filter> filters)
        {
            if (filter.Filters != null && filter.Filters.Any())
            {
                return "(" + string.Join(" " + filter.Logic + " ",
                    filter.Filters.Select(f => Transform(f, filters)).ToArray()) + ")";
            }
            int index = filters.IndexOf(filter);
            var comparison = Operators[filter.Operator];
            if (filter.Operator == "doesnotcontain")
            {
                return string.Format("({0} != null && !{0}.ToString().{1}(@{2}))",
                    filter.Field, comparison, index);
            }
            if (comparison == "StartsWith" ||
                comparison == "EndsWith" ||
                comparison == "Contains")
            {
                return string.Format("({0} != null && {0}.ToString().{1}(@{2}))",
                filter.Field, comparison, index);
            }
            return string.Format("{0} {1} @{2}", filter.Field, comparison, index);
        }
    }
}

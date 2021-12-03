using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Holiberry.Api.Common.Pagination.Extensions;

namespace Holiberry.Api.Common.Pagination
{
    public static class ServicePagedResponse
    {


        public static async Task<PagedResponse<T>> GetFilteredPagedResponse<T>(IQueryable<T> queries, PaginationFilter paginationFilter) where T : class
        {
            if (paginationFilter == null)
                throw new ArgumentNullException(nameof(paginationFilter));

            var filtersQuery = queries.ToFilterView(paginationFilter);

            var pageData = await GetPaginationData(filtersQuery, paginationFilter);

            var result = await filtersQuery
                .ToPaginationView(paginationFilter)
                .ToListAsync();

            return new PagedResponse<T>(result)
            {
                PagedData = pageData
            };
        }

        public static async Task<PagedResponse<TResponse>> GetFilteredPagedResponse<T, TResponse>(IQueryable<T> queries, PaginationFilter paginationFilter, IConfigurationProvider mapperConfigurationProvider) where T : class where TResponse : class
        {
            if (paginationFilter == null)
                throw new ArgumentNullException(nameof(paginationFilter));

            var filtersQuery = queries.ToFilterView(paginationFilter);

            var pageData = await GetPaginationData(filtersQuery, paginationFilter);

            var result = await filtersQuery
                .ToPaginationView(paginationFilter)
                .ProjectTo<TResponse>(mapperConfigurationProvider)
                .ToListAsync();

            return new PagedResponse<TResponse>(result)
            {
                PagedData = pageData
            };
        }






        private static async Task<PagedData> GetPaginationData<T>(IQueryable<T> queries, PaginationFilter paginationFilter) where T : class
        {
            var pagedData = new PagedData()
            {
                PageNumber = paginationFilter.CurrentPage,
                PageSize = paginationFilter.PerPage,
                ResultsCount = await queries.CountAsync()
            };

            double pages = (double)pagedData.ResultsCount / (double)pagedData.PageSize;
            if (double.IsInfinity(pages))
            {
                pagedData.Pages = 0;
            }
            else
            {
                pagedData.Pages = (int)pages;
                if (pages - (int)pages != 0)
                {
                    pagedData.Pages += 1;
                }
            }
            return pagedData;
        }
    }
}
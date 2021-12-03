using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Holiberry.Api.Common.Pagination.Extensions
{
    public static class ListExtensions
    {
        public static async Task<List<T>> ToPaginatedList<T>(this IQueryable<T> queries, PaginationFilter paginationFilter = null)
        {
            if (paginationFilter != null)
            {
                int skipRecords = (paginationFilter.CurrentPage - 1) * paginationFilter.PerPage;

                return await queries.Skip(skipRecords)
                        .Take(paginationFilter.PerPage)
                        .ToListAsync();
            }
            return await queries.ToListAsync();
        }
    }
}

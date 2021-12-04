using System;
using System.Collections.Generic;
using System.Dynamic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Holiberry.Api.Common.Pagination;

namespace Holiberry.Api.ViewModels.Pagination
{
    public class PaginationVM
    {
        public ResultPager Pager { get; set; }
        public ViewContext ViewContext { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public Dictionary<string, object> QueryString { get; set; }

        public PaginationVM(PagedData pager, ViewContext viewContext)
        {
            if (pager == null)
                throw new ArgumentNullException("PagedData");

            Pager = new ResultPager()
            {
                CurrentPage = pager.PageNumber ?? 0,
                ResultsPerPage = pager.PageSize ?? 0,
                Pages = pager.Pages ?? 0,
                ResultsCount = pager.ResultsCount ?? 0
            };
            ViewContext = viewContext;
            Action = viewContext.RouteData.Values["action"].ToString();
            Controller = viewContext.RouteData.Values["controller"].ToString();

            //Create base QueryString
            QueryString = new Dictionary<string, object>();
            foreach (var qs in viewContext.HttpContext.Request.Query)
            {
                QueryString[qs.Key] = qs.Value;
            }
        }


        public object GetRouteValuesForPage(int page)
        {
            QueryString["CurrentPage"] = page;

            return QueryString;
        }
    }
}
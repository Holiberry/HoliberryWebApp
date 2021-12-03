using System.Collections.Generic;
using System.Threading.Tasks;

namespace Holiberry.Api.Managers.ViewRender
{
    public interface IRazorViewRenderer
    {
        Task<string> RenderViewToStringAsync<TModel>(string viewName, TModel model);
        Task<string> RenderViewToStringAsync(string viewName, Dictionary<string, object> viewData = null);
    }
}

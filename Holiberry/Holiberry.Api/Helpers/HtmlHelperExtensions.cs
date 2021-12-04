using Holiberry.Api.Extensions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Holiberry.Api.Helpers
{
    public static class HtmlHelperExtensions
    {
        private const string _partialViewScriptItemPrefix = "scripts_";
        public static IHtmlContent PartialSectionScripts(this IHtmlHelper htmlHelper, Func<object, HelperResult> template)
        {
            htmlHelper.ViewContext.HttpContext.Items[_partialViewScriptItemPrefix + Guid.NewGuid()] = template;
            return new HtmlContentBuilder();
        }

        public static IHtmlContent RenderPartialSectionScripts(this IHtmlHelper htmlHelper)
        {
            var partialSectionScripts = htmlHelper.ViewContext.HttpContext.Items.Keys
                .Where(k => Regex.IsMatch(
                    k.ToString(),
                    "^" + _partialViewScriptItemPrefix + "([0-9A-Fa-f]{8}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{12})$"));
            var contentBuilder = new HtmlContentBuilder();
            foreach (var key in partialSectionScripts)
            {
                var template = htmlHelper.ViewContext.HttpContext.Items[key] as Func<object, HelperResult>;
                if (template != null)
                {
                    var writer = new System.IO.StringWriter();
                    template(null).WriteTo(writer, HtmlEncoder.Default);
                    contentBuilder.AppendHtml(writer.ToString());
                }
            }
            return contentBuilder;
        }


        public static IHtmlContent DisplayOrNoData(this IHtmlHelper htmlHelper, string html)
        {
            if (string.IsNullOrEmpty(html))
                return new HtmlString("<span class=\"kt-font-danger\">Brak</span>");
            return new HtmlString(html);

        }

        public static IHtmlContent DisplayTimeFromTicks(this IHtmlHelper htmlHelper, long? ticks) => ticks switch
        {
            null => new HtmlString(string.Empty),
            _ => new HtmlString(TimeSpan.FromTicks(ticks.Value).Format())
        };

        public static IHtmlContent GetIconForBoolErrorValue(this IHtmlHelper htmlHelper, bool? value) => value switch
        {
            true => new HtmlString("<i class=\"fa fa-check-circle text-success\"></i>"),
            false => new HtmlString("<i class=\"fa fa-circle-o text-danger\"></i>"),
            _ => new HtmlString("<i class=\"fa fa-circle-o\"></i>")
        };



    }
}

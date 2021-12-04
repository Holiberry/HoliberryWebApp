using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holiberry.Api.ViewModels.Common
{
    public class SubheaderVM
    {
        public string Title { get; set; }
        public BreadcrumbPage PrevPage { get; set; }
        public BreadcrumbPage PrevPrevPage { get; set; }
        public int? ResultCount { get; set; }

        //Action buttons
        public string CreateLink { get; set; }
        public string UpdateLink { get; set; }
        public string DeleteClassName { get; set; }
        public BreadcrumbOptionClassButton ExtraOptionClassButton { get; set; }
        public BreadcrumbPage ExtraOptionButton { get; set; }
        public BreadcrumbPage CreateRelation { get; set; }
    }

    public class BreadcrumbPage
    {
        public string Url { get; set; }
        public string Name { get; set; }

        public BreadcrumbPage() { }

        public BreadcrumbPage(string name, string url)
        {
            Url = url;
            Name = name;
        }
    }

    public class BreadcrumbOptionClassButton
    {
        public string ClassName { get; set; }
        public string Name { get; set; }

        public BreadcrumbOptionClassButton() { }

        public BreadcrumbOptionClassButton(string name, string className)
        {
            ClassName = className;
            Name = name;
        }
    }
}

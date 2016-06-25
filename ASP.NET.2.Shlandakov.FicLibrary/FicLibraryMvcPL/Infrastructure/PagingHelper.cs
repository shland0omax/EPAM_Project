using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using FicLibraryMvcPL.Models;

namespace FicLibraryMvcPL.Infrastructure
{
    public static class PagingHelper
    {
        public static MvcHtmlString Pagination(this HtmlHelper html,
        PageInfo pageInfo, string paginationFunc)
        {
            var paginationContainer = new TagBuilder("div");
            paginationContainer.MergeAttribute("class", "pagination align-center");
            var pages = new StringBuilder();
            for (int i = 1; i <= pageInfo.TotalPages; i++)
            {
                TagBuilder innerSpan = new TagBuilder("span");
                innerSpan.MergeAttribute("class", i == pageInfo.PageNumber ? "item current" : "item");
                innerSpan.MergeAttribute("onclick", i == pageInfo.PageNumber ? "" : $"{paginationFunc}({i})");
                innerSpan.InnerHtml = i.ToString();

                pages.Append(innerSpan.ToString(TagRenderMode.Normal));
            }
            paginationContainer.InnerHtml = pages.ToString();
            var res = paginationContainer.ToString(TagRenderMode.Normal);
            return MvcHtmlString.Create(res);
        }
    }
}
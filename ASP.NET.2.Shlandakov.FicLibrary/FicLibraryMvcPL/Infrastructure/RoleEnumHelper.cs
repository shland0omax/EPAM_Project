using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interfaces;
using FicLibraryMvcPL.Models;

namespace FicLibraryMvcPL.Infrastructure
{
    public static class RoleEnumHelper
    {
        public static MvcHtmlString RoleEnum(this HtmlHelper html, string userName, string editorName)
        {
            var select= new TagBuilder("select");
            select.MergeAttribute("id", "role");
            var userService = DependencyResolver.Current.GetService<IUserService>();
            var user = userService.GetUserByLogin(userName);
            var editor = editorName == null ? null : userService.GetUserByLogin(editorName);
            if (editor == null || user.RoleId <= editor.RoleId || editor.RoleId > 3)
            {
                select.MergeAttribute("style", "display:none");
                return new MvcHtmlString(select.ToString(TagRenderMode.Normal));
            }
            var options = "";
            for (int i = editor.RoleId +1; i <= Enum.GetNames(typeof(Role)).Length; i++)
            {
                var option = new TagBuilder("option");
                option.MergeAttribute("value",  i.ToString());
                option.SetInnerText(Enum.GetName(typeof(Role), i));
                options += option.ToString(TagRenderMode.Normal) + "\n";
            }
            select.MergeAttribute("data-val", "true");
            select.MergeAttribute("data-val-required", "The field is required.");
            select.MergeAttribute("name", "role");

            select.InnerHtml = options;
            return new MvcHtmlString(select.ToString(TagRenderMode.Normal));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace FicLibraryMvcPL.App_Start
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //Metro UI main script
            bundles.Add(new ScriptBundle("~/bundles/metro-ui")
                .Include("~/scripts/jquery-1.10.2.min.js")
                .Include("~/scripts/metro.min.js")
                .Include("~/scripts/jquery.unobtrusive-ajax.min.js")
                .Include("~/scripts/FicLibraryScripts.js")
                );

            //Metro UI main styles
            bundles.Add(new StyleBundle("~/Content/mentro-ui")
                .Include("~/Content/metro-icons.min.css")
                .Include("~/Content/metro-responsive.min.css")
                .Include("~/Content/metro-rtl.min.css")
                .Include("~/Content/metro-schemes.min.css")
                .Include("~/Content/metro.min.css")
                );
        }
    }
}
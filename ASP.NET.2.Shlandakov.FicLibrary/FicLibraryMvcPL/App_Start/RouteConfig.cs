using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FicLibraryMvcPL
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "User",
                url: "user/id{id}",
                defaults: new { controller = "User", action = "Index" }
            );

            routes.MapRoute("avatar", "user/uploadavatar", new {controller = "User", action = "AvatarUpload"},
                new {httpMethod = new HttpMethodConstraint("POST")});

            routes.MapRoute(
                name: "UserActions",
                url: "user/{profileId}/{action}",
                defaults: new { controller = "User", action = "Edit" }
            );

            routes.MapRoute(
                name: "Text",
                url: "text/{id}/{action}",
                defaults: new { controller = "Text", action = "Index" }
            );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "NotFound",
                url:"{*url}",
                defaults: new {controller = "Error", action = "Index"}
            );
        }
    }
}

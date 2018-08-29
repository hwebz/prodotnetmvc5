using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace UrlsAndRoutes
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // This route for testing with TestImcomingRoutes() test method
            //routes.MapRoute(
            //    name: "MyRoute",
            //    url: "{controller}/{action}"
            //);

            // This route for testing with TestImcomingRoutes() test method
            //routes.MapRoute(
            //    name: "MyOtherRoute",
            //    url: "{controller}/{action}",
            //    defaults: new { controller = "Home", action = "Index" }
            //);

            routes.MapRoute("ShopSchema2", "Shop/OldAction", new {controller = "Home", action = "Index"});

            routes.MapRoute("ShopSchema", "Shop/{action}", new {controller = "Home"});

            routes.MapRoute("", "X{controller}/{action}");

            routes.MapRoute("MyRoute", "{controller}/{action}/{id}/{*catchall}", new {controller = "Home", action = "Index", /*id = "DefaultId"*/ id = UrlParameter.Optional});

            routes.MapRoute("", "Public/{controller}/{action}", new {controller = "Home", action = "Index"});

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);

        }
    }
}

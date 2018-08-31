using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Routing.Constraints;
using System.Web.Routing;
using UrlsAndRoutes.Infrastructure;

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

            //routes.MapRoute("ShopSchema2", "Shop/OldAction", new {controller = "Home", action = "Index"});

            //routes.MapRoute("ShopSchema", "Shop/{action}", new {controller = "Home"});

            //routes.MapRoute("", "X{controller}/{action}");

            //routes.MapRoute("MyRoute", "{controller}/{action}/{id}/{*catchall}", new {controller = "Home", action = "Index", /*id = "DefaultId"*/ id = UrlParameter.Optional});
            //var myRoute = routes.MapRoute("AddControllerRoute", "Home/{action}/{id}/{*catchall}",
            //    new
            //    {
            //        controller = "Home",
            //        action = "Index",
            //        /*id = "DefaultId"*/
            //        id = UrlParameter.Optional
            //    },
            //    new
            //    {
            //        controller = "^H.*",
            //        action = "^Index$|^About$", // constraints
            //        httpMethod = new HttpMethodConstraint("GET", "POST"),
            //        //id = new RangeRouteConstraint(10, 20)
            //    },
            //    new[]
            //    {
            //        "UrlsAndRoutes.Controllers.AdditionalControllers"
            //    });
            //routes.MapRoute("ChromeRoute", "{*catchall}",
            //    new
            //    {
            //        controller = "Home",
            //        action = "Index"
            //    },
            //    new
            //    {
            //        customConstraint = new UserAgentConstraint("Chrome")
            //    },
            //    new[]
            //    {
            //        "UrlsAndRoutes.Controllers.AdditionalControllers"
            //    }); // redirect to default controller action if browser is Chrome, else return 403 - Forbidden
            //routes.MapRoute("MyRoute", "{controller}/{action}/{id}/{*catchall}", 
            //    new {
            //        controller = "Home",
            //        action = "Index", 
            //        /*id = "DefaultId"*/
            //        id = UrlParameter.Optional
            //    },
            //    new
            //    {
            //        controller = "^H.*",
            //        action = "^Index$|^About$", // constraints
            //        httpMethod = new HttpMethodConstraint("GET", "POST"),
            //        //id = new RangeRouteConstraint(10, 20)
            //        id = new CompoundRouteConstraint(new List<IRouteConstraint>
            //        {
            //            new AlphaRouteConstraint(),
            //            new MinLengthRouteConstraint(6)
            //        })
            //    },
            //    new[]
            //    {
            //        "UrlsAndRoutes.Controllers"
            //    });

            //routes.MapRoute("", "Public/{controller}/{action}", new {controller = "Home", action = "Index"});

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);

            //myRoute.DataTokens["UseNamespaceFallback"] = false;

            // ATTRIBUTE ROUTING
            routes.MapMvcAttributeRoutes();

            routes.MapRoute("Default", "{controller}/{action}/{id}",
                new {controller = "Home", action = "Index", id = UrlParameter.Optional},
                new[] {"UrlsAndRoutes.Controllers"});
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SMN.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "SaleSnap",
                url: "Sales/Snap",
                namespaces: new string[] { "SMN.Web.Controllers" },
                defaults: new { controller = "Sales", action = "Snap" }
            );

            routes.MapRoute(
                name: "SaleSoon",
                url: "Sales/Soon",
                namespaces: new string[] { "SMN.Web.Controllers" },
                defaults: new { controller = "Sales", action = "Soon" }
            );

            routes.MapRoute(
                name: "SaleDetails",
                url: "Sales/{id}",
                namespaces: new string[] { "SMN.Web.Controllers" },
                defaults: new { controller = "Sales", action = "Details" }
            );

            routes.MapRoute(
                name: "Checkout",
                url: "Checkout/{id}",
                namespaces: new string[] { "SMN.Web.Controllers" },
                defaults: new { controller = "Checkout", action = "Details" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                namespaces: new string[] { "SMN.Web.Controllers" },
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

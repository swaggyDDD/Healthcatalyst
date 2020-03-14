namespace Catalyst.Web
{
    using System.Web.Mvc;
    using System.Web.Routing;
    
    using Catalyst.Core;

    /// <summary>
    /// Application route configuration class.
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        /// Registers the application MVC routes.
        /// </summary>
        /// <param name="routes">
        /// The routes.
        /// </param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            // Ignores HttpHandler routes
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Person",
                url: Web.Constants.PersonRoute.EnsureNotStartsOrEndsWith('/') + "/{slug}",
                defaults: new { controller = "People", action = "PersonDetails", slug = UrlParameter.Optional });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }
    }
}

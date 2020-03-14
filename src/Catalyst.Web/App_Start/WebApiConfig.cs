namespace Catalyst.Web
{
    using System.Web.Http;

    /// <summary>
    /// WebAPI route configuration.
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Registers WebAPI routes.
        /// </summary>
        /// <param name="config">
        /// The config.
        /// </param>
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
        }
    }
}

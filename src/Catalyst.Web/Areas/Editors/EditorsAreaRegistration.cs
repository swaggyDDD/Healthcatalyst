namespace Catalyst.Web.Areas.Editors
{
    using System.Web.Mvc;

    /// <summary>
    /// The MVC area for property editors.
    /// </summary>
    public class EditorsAreaRegistration : AreaRegistration 
    {
        /// <summary>
        /// Gets the area name.
        /// </summary>
        public override string AreaName => "Editors";

        /// <summary>
        /// Registers the area.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Editors_default",
                "Editors/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional });
        }
    }
}
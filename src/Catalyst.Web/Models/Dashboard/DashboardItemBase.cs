namespace Catalyst.Web.Models.Dashboard
{
    using Catalyst.Core;

    /// <summary>
    /// Represents a base UI box model.
    /// </summary>
    public abstract class DashboardItemBase
    {
        /// <summary>
        /// Gets the box title.
        /// </summary>
        public abstract string Title { get; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Gets or sets the alias for the route.
        /// </summary>
        /// <remarks>
        /// Have to instantiate here because of model check
        /// </remarks>
        public string AjaxRouteAlias { get; set; } = string.Empty;
        
        /// <summary>
        /// Gets or sets a value indicating whether this item gets it's data asynchronously.
        /// </summary>
        public bool IsAsync => !this.AjaxRouteAlias.IsNullOrWhiteSpace();
    }
}
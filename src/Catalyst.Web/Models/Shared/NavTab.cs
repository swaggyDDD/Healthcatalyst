namespace Catalyst.Web.Models.Shared
{
    /// <summary>
    /// Represents a navigation tab.
    /// </summary>
    public class NavTab
    {
        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the target.
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is current.
        /// </summary>
        public bool IsCurrent { get; set; }
    }
}
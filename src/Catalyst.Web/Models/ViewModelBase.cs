namespace Catalyst.Web.Models
{
    using Catalyst.Web.Models.Shared;

    /// <summary>
    /// Represents a view model.
    /// </summary>
    public class ViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModel"/> class.
        /// </summary>
        protected ViewModel()
        {
            this.Meta = new Meta();
        }

        /// <summary>
        /// Gets or sets the page <see cref="Meta"/>.
        /// </summary>
        public Meta Meta { get; set; }

        /// <summary>
        /// Gets or sets the current url.
        /// </summary>
        public NavTab CurrentTab { get; set; }
    }
}
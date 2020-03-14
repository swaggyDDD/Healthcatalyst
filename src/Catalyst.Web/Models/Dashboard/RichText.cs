namespace Catalyst.Web.Models.Dashboard
{
    using System.Web;

    /// <summary>
    /// Represents a rich text dashboard item.
    /// </summary>
    public class RichText : DashboardItemBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RichText"/> class.
        /// </summary>
        /// <param name="title">
        /// The title.
        /// </param>
        public RichText(string title)
        {
            this.Title = title;
        }

        /// <inheritdoc />
        public override string Title { get; }

        /// <summary>
        /// Gets or sets the markdown file.
        /// </summary>
        public string MarkdownFile { get; set; }

        /// <summary>
        /// Gets or sets the directory.
        /// </summary>
        public string Directory { get; set; }
    }
}
namespace Catalyst.Web.Areas.Editors.Models
{
    using System;
    using Catalyst.Web.Models.Dashboard;

    /// <summary>
    /// The editor form base.
    /// </summary>
    public abstract class EditorFormBase : DashboardItemBase, IEditorForm
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditorFormBase"/> class.
        /// </summary>
        /// <param name="title">
        /// The title.
        /// </param>
        protected EditorFormBase(string title)
        {
            Title = title;
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid PersonId { get; set; }

        /// <summary>
        /// Gets title.
        /// </summary>
        public override string Title { get; }

        /// <summary>
        /// Gets or sets the return url.
        /// </summary>
        public string ReturnUrl { get; set; }

    }
}
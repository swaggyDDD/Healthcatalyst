namespace Catalyst.Web.Areas.Editors.Models
{
    using System;

    /// <summary>
    /// Represents an editor form.
    /// </summary>
    public interface IEditorForm
    {
        /// <summary>
        /// Gets or sets the person id.
        /// </summary>
        Guid PersonId { get; set; }

        /// <summary>
        /// Gets or sets the return url.
        /// </summary>
        string ReturnUrl { get; set; }
    }
}
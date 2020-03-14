namespace Catalyst.Web.Areas.Editors.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    /// <summary>
    /// Model for the photo editor.
    /// </summary>
    public class PhotoEditor : EditorFormBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PhotoEditor"/> class.
        /// </summary>
        public PhotoEditor()
            : this("Saving ...")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PhotoEditor"/> class.
        /// </summary>
        /// <param name="title">
        /// The title.
        /// </param>
        public PhotoEditor(string title)
            : base(title)
        {
        }

        /// <summary>
        /// Gets or sets the photo url.
        /// </summary>
        public string PhotoUrl { get; set; }

        /// <summary>
        /// Gets or sets the posted file.
        /// </summary>
        [Display(Name = "Select File")]
        public HttpPostedFileBase PostedFile { get; set; }
    }
}
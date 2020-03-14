namespace Catalyst.Web.Areas.Editors.Models
{
    using System.ComponentModel.DataAnnotations;

    using Catalyst.Core.Models.PropData;

    /// <summary>
    /// Model for the interest listing editor
    /// </summary>
    public class InterestListEditor : EditorFormBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InterestListEditor"/> class.
        /// </summary>
        public InterestListEditor()
            : this("Saving ...")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InterestListEditor"/> class.
        /// </summary>
        /// <param name="title">
        /// The title.
        /// </param>
        public InterestListEditor(string title)
            : base(title)
        {
        }

        /// <summary>
        /// Gets or sets the interest name.
        /// </summary>
        [Display(Name = "Interset Name")]
        [Required(ErrorMessage = " * required.")]
        public string InterestName { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        [Display(Name = "Url")]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the interest list.
        /// </summary>
        public InterestList InterestList { get; set; }
    }
}
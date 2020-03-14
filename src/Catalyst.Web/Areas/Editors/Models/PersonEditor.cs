namespace Catalyst.Web.Areas.Editors.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Catalyst.Core.Models.Domain;

    /// <summary>
    /// Represents a model for adding and editing <see cref="IPerson"/>.
    /// </summary>
    public class PersonEditor : EditorFormBase
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonEditor"/> class.
        /// </summary>
        public PersonEditor()
            : this("Saving....")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonEditor"/> class.
        /// </summary>
        /// <param name="title">
        /// The title.
        /// </param>
        public PersonEditor(string title)
            : base(title)
        {
        }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        [Display(Name = "First name"), Required(ErrorMessage = " * required.")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        [Display(Name = "Last name"), Required(ErrorMessage = " * required.")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the birthday.
        /// </summary>
        [Display(Name = "Birthday"), Required(ErrorMessage = " * required.")]
        public string Birthday { get; set; } = DateTime.Today.ToShortDateString();

        /// <summary>
        /// Gets or sets a value indicating whether to watch this person.
        /// </summary>
        [Display(Name = "Watch this person")]
        public bool Watch { get; set; }
    }
}
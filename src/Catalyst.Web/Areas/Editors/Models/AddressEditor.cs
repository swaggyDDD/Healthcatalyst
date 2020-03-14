namespace Catalyst.Web.Areas.Editors.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using Catalyst.Core.Models.Domain;

    /// <summary>
    /// Represents a model for editing addresses.
    /// </summary>
    public class AddressEditor : EditorFormBase, IAddress
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddressEditor"/> class.
        /// </summary>
        public AddressEditor()
            : this("Saving...")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddressEditor"/> class.
        /// </summary>
        /// <param name="title">
        /// The title.
        /// </param>
        public AddressEditor(string title)
            : base(title)
        {
        }

        /// <summary>
        /// Gets or sets the address id.
        /// </summary>
        public Guid AddressId { get; set; }

        /// <inheritdoc />
        [Display(Name = "Descriptive name for the address")]
        [Required(ErrorMessage = " * required.")]
        public string Name { get; set; }

        /// <inheritdoc />
        [Display(Name = "Address line 1")]
        [Required(ErrorMessage = " * required.")]
        public string Address1 { get; set; }

        /// <inheritdoc />
        [Display(Name = "Address line 2")]
        public string Address2 { get; set; }

        /// <inheritdoc />
        [Display(Name = "City or locality")]
        [Required(ErrorMessage = " * required.")]
        public string Locality { get; set; }

        /// <inheritdoc />
        [Display(Name = "State or province")]
        public string Region { get; set; }

        /// <inheritdoc />
        [Display(Name = "Postal code")]
        public string PostalCode { get; set; }

        /// <inheritdoc />
        [Display(Name = "Country")]
        [Required(ErrorMessage = " * required.")]
        public string CountryCode { get; set; }

        /// <summary>
        /// Gets or sets the countries.
        /// </summary>
        public IEnumerable<SelectListItem> Countries { get; set; }
    }
}
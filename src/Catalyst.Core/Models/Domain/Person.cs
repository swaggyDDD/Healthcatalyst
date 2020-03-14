namespace Catalyst.Core.Models.Domain
{
    using System;
    using System.Collections.Generic;

    using Catalyst.Core.Data;

    /// <summary>
    /// Represents a person DTO.
    /// </summary>
    public class Person : EntityBase, IPerson, IDto
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Person"/> class.
        /// </summary>
        public Person()
        {
            this.Id = GuidComb.GenerateComb();
            this.Initialize();
        }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the slug.
        /// </summary>
        public string Slug { get; set; }

        /// <summary>
        /// Gets or sets the birthday.
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <inheritdoc />
        public bool Watch { get; set; }

        /// <summary>
        /// Gets or sets the addresses.
        /// </summary>
        public virtual ICollection<Address> Addresses { get; set; }

        /// <summary>
        /// Gets or sets the properties.
        /// </summary>
        public virtual ICollection<ExtendedProperty> Properties { get; set; }

        /// <summary>
        /// The initialize.
        /// </summary>
        private void Initialize()
        {
            Addresses = new List<Address>();
            Properties = new List<ExtendedProperty>();
        }
    }
}
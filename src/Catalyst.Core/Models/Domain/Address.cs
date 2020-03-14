namespace Catalyst.Core.Models.Domain
{
    using System;

    using Catalyst.Core.Data;

    /// <summary>
    /// Represents an street or postal address.
    /// </summary>
    public class Address : EntityBase, IAddress, IDto
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Address"/> class.
        /// </summary>
        public Address()
        {
            this.Id = GuidComb.GenerateComb();
        }

        /// <inheritdoc />
        public string Name { get; set; }

        /// <inheritdoc />
        public string Address1 { get; set; }

        /// <inheritdoc />
        public string Address2 { get; set; }

        /// <inheritdoc />
        public string Locality { get; set; }

        /// <inheritdoc />
        public string Region { get; set; }

        /// <inheritdoc />
        public string PostalCode { get; set; }

        /// <inheritdoc />
        public string CountryCode { get; set; }

        /// <summary>
        /// Gets or sets the person associated with the address.
        /// </summary>
        /// <remarks>
        /// Foreign key
        /// </remarks>
        public virtual Person Person { get; set; }

        ///// <summary>
        ///// Gets or sets the person id.
        ///// </summary>
        //internal Guid PersonId { get; set; }
    }
}
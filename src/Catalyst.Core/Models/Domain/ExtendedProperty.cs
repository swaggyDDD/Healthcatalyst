namespace Catalyst.Core.Models.Domain
{
    using System;

    using Catalyst.Core.Data;

    /// <summary>
    /// Represents an extended property
    /// </summary>
    public class ExtendedProperty : EntityBase, IExtendedProperty
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedProperty"/> class.
        /// </summary>
        public ExtendedProperty()
        {
            this.Id = GuidComb.GenerateComb();
        }

        /// <inheritdoc />
        public string ConverterAlias { get; set; }

        /// <inheritdoc />
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the person associated with the property.
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
namespace Catalyst.Core.Models.Domain
{
    using System;

    /// <summary>
    /// Represents an entity DTO.
    /// </summary>
    internal interface IDto
    {
        /// <summary>
        /// Gets or sets the id of the Entity.
        /// </summary>
        Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the date the entity was last updated.
        /// </summary>                                                             s
        DateTime UpdateDate { get; set; }

        /// <summary>
        /// Gets or sets the initial creation date of the entity record.
        /// </summary>
        DateTime CreateDate { get; set; }
    }
}
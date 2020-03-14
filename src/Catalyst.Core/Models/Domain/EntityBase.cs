namespace Catalyst.Core.Models.Domain
{
    using System;

    /// <inheritdoc />
    public abstract class EntityBase : IEntity
    {
        /// <inheritdoc />
        public Guid Id { get; set; }

        /// <inheritdoc />
        public DateTime UpdateDate { get; set; }

        /// <inheritdoc />
        public DateTime CreateDate { get; set; }
    }
}
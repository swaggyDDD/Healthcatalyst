namespace Catalyst.Core.Events
{
    using System;

    using Catalyst.Core.Models.Domain;

    /// <summary>
    /// Event arguments for entity related events.
    /// </summary>
    /// <typeparam name="TEntity">
    /// The type of the entity
    /// </typeparam>
    public class EntityEventArgs<TEntity> : EventArgs
        where TEntity : IEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityEventArgs{TEntity}"/> class.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        public EntityEventArgs(TEntity entity)
        {
            Entity = entity;
        }

        /// <summary>
        /// Gets the entity.
        /// </summary>
        public TEntity Entity { get; }
    }
}
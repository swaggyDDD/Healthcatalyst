namespace Catalyst.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    using Catalyst.Core.Models.Domain;

    /// <summary>
    /// Represents a service based off <see cref="DbContext"/>.
    /// </summary>
    /// <typeparam name="TEntity">
    /// The type of <see cref="IEntity"/>
    /// </typeparam>
    public interface ISimpleDbContextCrudService<TEntity> : IService
        where TEntity : class, IEntity
    {
        /// <summary>
        /// Gets the count of all entities managed by the service.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int Count();

        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <param name="ids">
        /// Optional collection of Ids.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{TEntity}"/>.
        /// </returns>
        IEnumerable<TEntity> GetAll(params Guid[] ids);

        /// <summary>
        /// Gets an entity by it's Id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="lazy">
        /// Lazy property load.
        /// </param>
        /// <returns>
        /// The <see cref="TEntity"/>.
        /// </returns>
        TEntity Get(Guid id, bool lazy = true);

        /// <summary>
        /// Saves the entity.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <param name="noStateCheck">
        /// A value indicating to ignore the parent entity state check
        /// </param>
        void Save(TEntity entity, bool noStateCheck = false);

        /// <summary>
        /// Deletes the entity.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        void Delete(TEntity entity);
    }
}
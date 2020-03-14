namespace Catalyst.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    using Catalyst.Core.Caching;
    using Catalyst.Core.Data.Context;
    using Catalyst.Core.Events;
    using Catalyst.Core.Logging;
    using Catalyst.Core.Models.Domain;

    /// <summary>
    /// Represents a service based on the <see cref="ICatalystDbContext"/>.
    /// </summary>
    /// <typeparam name="TEntity">
    /// The type of <see cref="IEntity"/>
    /// </typeparam>
    internal abstract class CatalystDbContextServiceBase<TEntity> : DbContextServiceBase<CatalystDbContext, TEntity>, ISimpleDbContextCrudService<TEntity>
        where TEntity : class, IEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CatalystDbContextServiceBase{TEntity}"/> class. 
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <param name="cache">
        /// The cache.
        /// </param>
        /// <param name="logger">
        /// The logger.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Throws on any argument null
        /// </exception>
        protected CatalystDbContextServiceBase(CatalystDbContext context, ICacheManager cache, ILogger logger)
            : base(context, cache, logger)
        {
        }

        #region events

        /// <summary>
        /// Occurs before adding a new entity.
        /// </summary>
        protected event EventHandler<EntityEventArgs<TEntity>> Adding;

        /// <summary>
        /// Occurs after adding a new entity.
        /// </summary>
        protected event EventHandler<EntityEventArgs<TEntity>> Added;

        /// <summary>
        /// Occurs before saving an entity.
        /// </summary>
        protected event EventHandler<EntityEventArgs<TEntity>> Saving;

        /// <summary>
        /// Occurs after saving an entity.
        /// </summary>
        protected event EventHandler<EntityEventArgs<TEntity>> Saved;

        /// <summary>
        /// Occurs before deleting an entity.
        /// </summary>
        protected event EventHandler<EntityEventArgs<TEntity>> Deleting;

        /// <summary>
        /// Occurs after deleting an entity.
        /// </summary>
        protected event EventHandler<EntityEventArgs<TEntity>> Deleted;

        #endregion


        /// <summary>
        /// Gets an entity by it's id.
        /// </summary>
        /// <param name="id">
        /// The id of the entity.
        /// </param>
        /// <param name="lazy">
        /// Lazy properties.
        /// </param>
        /// <returns>
        /// The <see cref="TEntity"/>.
        /// </returns>
        public virtual TEntity Get(Guid id, bool lazy = true)
        {
            var cacheKey = GetCacheKey(id);

            if (lazy)
            {
                return PerformGet(id);
            }

            var result = RuntimeCache.GetCacheItem(cacheKey);

            if (result != null) return (TEntity)result;

            // cache for 5 minutes
            return (TEntity)RuntimeCache.GetCacheItem(cacheKey, () => PerformGet(id, false), TimeSpan.FromMinutes(5));
        }

        /// <inheritdoc />
        public virtual IEnumerable<TEntity> GetAll(params Guid[] ids)
        {
            if (!ids.Any())
            {
                return this.Context.AsNoTracking();
            }

            return this.Context.AsNoTracking().Where(x => ids.Contains(x.Id));
        }

        /// <inheritdoc />
        public virtual void Save(TEntity entity, bool noStateCheck = false)
        {
            var state = GetEntityState(entity);

            if (state == EntityState.Unchanged && noStateCheck) state = EntityState.Modified;

            switch (state)
            {
                case EntityState.Added:
                    Emit(Adding, entity);
                    this.Context.Add(entity);
                    Emit(Added, entity);

                    Emit(Saving, entity);
                    DbContext.SaveChanges();
                    Emit(Saved, entity);
                    break;
                case EntityState.Detached:
                    this.Context.Attach(entity);

                    Emit(Saving, entity);
                    DbContext.SaveChanges();
                    Emit(Saved, entity);
                    break;
                case EntityState.Modified:

                    Emit(Saving, entity);
                    DbContext.SaveChanges();
                    Emit(Saved, entity);
                    
                    break;
                case EntityState.Deleted:
                case EntityState.Unchanged:
                default:
                    return;
            }

            RuntimeCache.ClearCacheItem(GetCacheKey(entity.Id));
        }

        /// <inheritdoc />
        public virtual void Delete(TEntity entity)
        {
            var dto = this.Context.Find(entity.Id);
            if (dto != null)
            {
                Emit(Deleting, entity);
                this.Context.Remove(dto);
                Emit(Deleted, entity);

                DbContext.SaveChanges();
            }

            RuntimeCache.ClearCacheItem(GetCacheKey(entity.Id));
        }

        /// <inheritdoc />
        public virtual int Count()
        {
            return this.Context.AsNoTracking().Count();
        }

        /// <summary>
        /// Performs the actual work of getting the entity.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="lazy">
        /// Use lazy properties.
        /// </param>
        /// <returns>
        /// The <see cref="TEntity"/>.
        /// </returns>
        protected virtual TEntity PerformGet(Guid id, bool lazy = true)
        {
            return this.Context.Find(id);
        }

        /// <summary>
        /// Gets the service cache key for the entity.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        protected string GetCacheKey(Guid id)
        {
            return $"{typeof(TEntity).Name}.{id}";
        }



        /// <summary>
        /// Attaches or gets an entity.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// A value indicating if the entity is currently attached.
        /// </returns>
        protected EntityState GetEntityState(TEntity entity)
        {
            var entry = DbContext.ChangeTracker.Entries<TEntity>().FirstOrDefault(e => e.Entity.Id == entity.Id);

            return entry == null ? EntityState.Added : entry.State;
        }

        /// <summary>
        /// Emits an event.
        /// </summary>
        /// <param name="handler">
        /// The <see cref="EventHandler"/>.
        /// </param>
        /// <param name="entity">
        /// The entity.
        /// </param>
        private void Emit(EventHandler<EntityEventArgs<TEntity>> handler, TEntity entity)
        {
            handler?.Invoke(this, new EntityEventArgs<TEntity>(entity));
        }
    }
}
namespace Catalyst.Core.Services
{
    using System;
    using System.Data.Entity;

    using Catalyst.Core.Caching;
    using Catalyst.Core.Data.Context;
    using Catalyst.Core.Logging;
    using Catalyst.Core.Models.Domain;

    /// <summary>
    /// Represents the a base service context.
    /// </summary>
    /// <typeparam name="TContext">
    /// The type of the context
    /// </typeparam>
    /// <typeparam name="TEntity">
    /// The type of the entity
    /// </typeparam>
    public abstract class DbContextServiceBase<TContext, TEntity>
        where TEntity : class, IEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DbContextServiceBase{TContext,TEntity}"/> class.
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
        protected DbContextServiceBase(TContext context, ICacheManager cache, ILogger logger)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (cache == null) throw new ArgumentNullException(nameof(cache));
            if (logger == null) throw new ArgumentNullException(nameof(logger));

            DbContext = context;
            CacheManager = cache;
            Logger = logger;
        }

        /// <summary>
        /// Gets or sets the <see cref="DbContext"/>.
        /// </summary>
        protected TContext DbContext { get; set; }

        /// <summary>
        /// Gets the <see cref="CatalystDbContext"/>.
        /// </summary>
        protected abstract DbSet<TEntity> Context { get; }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        protected ILogger Logger { get; }

        /// <summary>
        /// Gets the cache manager.
        /// </summary>
        protected ICacheManager CacheManager { get; }

        /// <summary>
        /// The runtime cache.
        /// </summary>
        protected IRuntimeCacheProvider RuntimeCache => CacheManager.RuntimeCache;

        /// <summary>
        /// Gets the entity set name.
        /// </summary>
        protected abstract string EntitySetName { get; }
    }
}
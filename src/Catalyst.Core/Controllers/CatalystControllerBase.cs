namespace Catalyst.Core.Controllers
{
    using System;
    using System.Web.Mvc;

    using Catalyst.Core.DI;
    using Catalyst.Core.Logging;
    using Catalyst.Core.Services;

    /// <summary>
    /// Represents a base MVC controller for the catalyst context.
    /// </summary>
    public abstract class CatalystControllerBase : Controller
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CatalystControllerBase"/> class.
        /// </summary>
        protected CatalystControllerBase()
            : this(Active.Logger, Active.Services)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CatalystControllerBase"/> class.
        /// </summary>
        /// <param name="logger">
        /// The logger.
        /// </param>
        /// <param name="services">
        /// The services.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Throws an exception if the <see cref="IServiceContext"/> is null    
        /// </exception>
        protected CatalystControllerBase(ILogger logger, IServiceContext services)
        {
            if (logger == null) throw new ArgumentNullException(nameof(logger));
            if (services == null) throw new ArgumentNullException(nameof(services));

            this.Logger = logger;
            this.Services = services;
        }

        /// <summary>
        /// Gets the <see cref="IServiceContext"/>.
        /// </summary>
        protected IServiceContext Services { get; }

        /// <summary>
        /// Gets the <see cref="ILogger"/>.
        /// </summary>
        protected ILogger Logger { get; }
    }
}
namespace Catalyst.Core.Services
{
    using System.Data.Entity;

    using Catalyst.Core.Caching;
    using Catalyst.Core.Data.Context;
    using Catalyst.Core.Logging;
    using Catalyst.Core.Models.Domain;

    using LightInject;

    /// <inheritdoc />
    internal partial class AddressService : DbContextServiceBase<CatalystDbContext, Address>, IAddressService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddressService"/> class.
        /// </summary>
        /// <param name="context">
        /// The <see cref="CatalystDbContext"/>.
        /// </param>
        /// <param name="cache">
        /// The <see cref="ICacheManager"/>.
        /// </param>
        /// <param name="logger">
        /// The <see cref="ILogger"/>.
        /// </param>
        public AddressService([Inject(Constants.Database.ConnectionStringName)]CatalystDbContext context, ICacheManager cache, ILogger logger)
            : base(context, cache, logger)
        {
        }

        /// <inheritdoc />
        protected override DbSet<Address> Context => DbContext.Set<Address>();

        /// <inheritdoc />
        protected override string EntitySetName => "Addresses";
    }
}
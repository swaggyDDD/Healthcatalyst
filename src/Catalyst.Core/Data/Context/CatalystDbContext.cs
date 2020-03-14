namespace Catalyst.Core.Data.Context
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Validation;
    using System.Linq;

    using Catalyst.Core.Logging;
    using Catalyst.Core.Models.Domain;
    using Catalyst.Core.Registers;

    /// <summary>
    /// Represents a database context for the Catalyst Coding Problem.
    /// </summary>
    public class CatalystDbContext : DbContext, ICatalystDbContext
    {
        /// <summary>
        /// The register for the model type configurations.
        /// </summary>
        private readonly IMappingConfigurationRegister _register;

        /// <summary>
        /// The <see cref="ILogger"/>.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CatalystDbContext"/> class.
        /// </summary>
        /// <param name="nameOrConnectionString">
        /// The name or connection string.
        /// </param>
        /// <param name="logger">
        /// The logger.
        /// </param>
        /// <param name="register">
        /// The register.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Throws if the register is null
        /// </exception>
        public CatalystDbContext(string nameOrConnectionString, ILogger logger, IMappingConfigurationRegister register)
            : base(nameOrConnectionString)
        {                   
            if (register == null) throw new ArgumentNullException(nameof(register));
            if (logger == null) throw new ArgumentException(nameof(logger));

            _register = register;
            _logger = logger;

            // Register the event to update last modified date
            var objectContext = ((IObjectContextAdapter)this).ObjectContext;
            objectContext.SavingChanges += (sender, args) =>
            {
                var now = DateTime.UtcNow;
                foreach (var entry in this.ChangeTracker.Entries<EntityBase>())
                {
                    var entity = entry.Entity;
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entity.CreateDate = now;
                            entity.UpdateDate = now;
                            break;
                        case EntityState.Modified:
                            entity.UpdateDate = now;
                            break;
                    }
                }

                this.ChangeTracker.DetectChanges();
            };

            Configuration.LazyLoadingEnabled = true;
        }

        /// <inheritdoc />
        public DbSet<Person> People { get; set; }

        /// <summary>
        /// Overrides Save changes for logging.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        /// <exception cref="DbEntityValidationException">
        /// Throws the context exception after the exception is logged.
        /// </exception>
        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                var improved = new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);

                _logger.Error<CatalystDbContext>("Context FAILED to SaveChanges", improved);

                throw improved;
            }
        }

        /// <inheritdoc />
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            foreach (var configuration in _register.GetInstantiations())
            {
                modelBuilder.Configurations.Add(configuration);
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
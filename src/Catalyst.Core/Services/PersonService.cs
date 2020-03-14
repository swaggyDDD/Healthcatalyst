namespace Catalyst.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Core.Objects;
    using System.Linq;

    using Catalyst.Core.Caching;
    using Catalyst.Core.Data.Context;
    using Catalyst.Core.Events;
    using Catalyst.Core.Logging;
    using Catalyst.Core.Models.Domain;

    using LightInject;

    /// <inheritdoc />
    internal partial class PersonService : CatalystDbContextServiceBase<Person>, IPersonService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PersonService"/> class.
        /// </summary>
        /// <param name="context">
        /// The <see cref="ICatalystDbContext"/>.
        /// </param>
        /// <param name="cache">
        /// The <see cref="ICacheManager"/>.
        /// </param>
        /// <param name="logger">
        /// The <see cref="ILogger"/>.
        /// </param>
        public PersonService([Inject(Constants.Database.ConnectionStringName)]CatalystDbContext context, ICacheManager cache, ILogger logger)
            : base(context, cache, logger)
        {
            // Fill in the slug on add
            this.Adding += (s, e) => { e.Entity.Slug = GetUniqueSlug(e.Entity); };
            this.Saving += (s, e) =>
                {
                    if (!e.Entity.Slug.StartsWith(e.Entity.GenerateSlug()))
                    {
                        e.Entity.Slug = GetUniqueSlug(e.Entity);
                    }
                };
        }


        /// <summary>
        /// The <see cref="DbSet{Person}"/>.
        /// </summary>
        protected override DbSet<Person> Context => DbContext.People;

        /// <summary>
        /// The entity set name.
        /// </summary>
        protected override string EntitySetName => "People";

        /// <inheritdoc />
        public Person Create(string firstName, string lastName, DateTime birthDay)
        {
            return new Person { FirstName = firstName, LastName = lastName, Birthday = birthDay };
        }

        /// <inheritdoc />
        public Person GetBySlug(string slug)
        {
            return this.Context.FirstOrDefault(x => x.Slug.Equals(slug, StringComparison.InvariantCultureIgnoreCase));
        }

        /// <inheritdoc />
        public IEnumerable<Person> SearchNames(string match, int maxResultCount = 5)
        {
            var terms = match.Split(' ').Select(x => x.Trim());

            var predicate = PredicateBuilder.False<Person>();

            foreach (var term in terms)
            {
                predicate = predicate.Or(x => x.FirstName.Contains(term) || x.LastName.Contains(term));
            }

            return Context.AsNoTracking().Where(predicate).OrderBy(p => p.FirstName).Take(maxResultCount);
        }

        /// <summary>
        /// Get's a unique slug for the <see cref="IPerson"/>.
        /// </summary>
        /// <param name="person">
        /// The person.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        internal string GetUniqueSlug(IPerson person)
        {
            var attempt = 0;
            while (!EnsureUniqueSlug(person.GenerateSlug(attempt)))
            {
                attempt++;
            }

            return person.GenerateSlug(attempt);
        }

        /// <summary>
        /// Ensures the slug is unique.
        /// </summary>
        /// <param name="slug">
        /// The slug.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        internal bool EnsureUniqueSlug(string slug)
        {
            return this.Context.FirstOrDefault(x => x.Slug.Equals(slug, StringComparison.InvariantCultureIgnoreCase)) == null;
        }

        /// <inheritdoc />
        protected override Person PerformGet(Guid id, bool lazy = true)
        {
            return lazy ? 
                
                Context.Find(id) : 

                Context
                .Include(p => p.Addresses)
                .Include(p => p.Properties)
                .FirstOrDefault(x => x.Id == id);
        }
    }
}
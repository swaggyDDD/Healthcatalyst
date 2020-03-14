namespace Catalyst.Core.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using Catalyst.Core.Models.Domain;

    /// <inheritdoc />
    internal partial class PersonService : IPersonQueryService
    {
        /// <inheritdoc />
        public IEnumerable<Person> GetRecentlyUpdated(int count = 5)
        {
            return this.Context.AsNoTracking()
                .OrderByDescending(x => x.UpdateDate)
                .Take(count)
                .ToArray()
                .Select(x => x.Id)
                .Select(id => Get(id));
        }

        /// <inheritdoc />
        public IEnumerable<Person> GetWatched()
        {
            return this.Context.AsNoTracking().Where(x => x.Watch).Select(x => x.Id).ToArray().Select(id => Get(id));
        }
    }
}

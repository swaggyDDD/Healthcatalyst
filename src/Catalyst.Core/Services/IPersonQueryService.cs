namespace Catalyst.Core.Services
{
    using System.Collections.Generic;

    using Catalyst.Core.Models.Domain;

    /// <summary>
    /// Represents a service for querying people.
    /// </summary>
    public interface IPersonQueryService
    {
        /// <summary>
        /// The get recently updated.
        /// </summary>
        /// <param name="count">
        /// The count.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{Person}"/>.
        /// </returns>
        IEnumerable<Person> GetRecentlyUpdated(int count = 5);

        /// <summary>
        /// Gets a collection of watched people.
        /// </summary>
        /// <returns>
        /// A collection of watched people.
        /// </returns>
        IEnumerable<Person> GetWatched();

        /// <summary>
        /// Searches the first and last name fields for a value.
        /// </summary>
        /// <param name="match">
        /// The value to "match".
        /// </param>
        /// <param name="maxResultCount">
        /// The number of results to return.
        /// </param>
        /// <returns>
        /// The collection of matching <see cref="Person"/>.
        /// </returns>
        IEnumerable<Person> SearchNames(string match, int maxResultCount = 5);
    }
}
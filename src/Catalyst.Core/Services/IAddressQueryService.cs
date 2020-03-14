namespace Catalyst.Core.Services
{
    using System.Collections.Generic;

    using Catalyst.Core.Models;

    /// <summary>
    /// Represents a service for querying addresses.
    /// </summary>
    public interface IAddressQueryService
    {
        /// <summary>
        /// Gets the number of people associated with a country.
        /// </summary>
        /// <param name="countryCode">
        /// The country code.
        /// </param>
        /// <returns>
        /// The count of people.
        /// </returns>
        int CountPeopleAddressInCountry(string countryCode);

        /// <summary>
        /// Gets a collection of all countries associated with persisted addresses.
        /// </summary>
        /// <param name="count">
        /// The number of countries to take.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{ICountry}"/>.
        /// </returns>
        IEnumerable<ICountry> GetAssociatedCountries(int count = int.MaxValue);

        /// <summary>
        /// Gets a collection of all  <see cref="ICountry"/>
        /// </summary>
        /// <returns>
        /// The collection of all countries.
        /// </returns>
        IEnumerable<ICountry> GetAllCountries();
    }
}
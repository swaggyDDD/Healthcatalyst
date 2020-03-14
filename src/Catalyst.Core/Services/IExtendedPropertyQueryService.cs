namespace Catalyst.Core.Services
{
    using Catalyst.Core.Models.Domain;

    /// <summary>
    /// Represents a service for querying <see cref="IExtendedProperty"/> entities.
    /// </summary>
    public interface IExtendedPropertyQueryService
    {
        /// <summary>
        /// The count people using.
        /// </summary>
        /// <param name="converterAlias">
        /// The converter alias.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int CountPeopleUsing(string converterAlias);
    }
}
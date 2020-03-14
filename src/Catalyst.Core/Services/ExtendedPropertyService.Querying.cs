namespace Catalyst.Core.Services
{
    using System;
    using System.Linq;

    /// <inheritdoc />
    internal partial class ExtendedPropertyService : IExtendedPropertyQueryService
    {
        /// <inheritdoc />
        public int CountPeopleUsing(string converterAlias)
        {
            // this works since the property cannot be associated more than once per person.
            return
                Context
                    .AsNoTracking()
                    .Count(x => x.ConverterAlias.Equals(converterAlias, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}

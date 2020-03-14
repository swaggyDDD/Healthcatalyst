namespace Catalyst.Core.ValueConverters
{
    using Catalyst.Core.Models.Domain;
    using Catalyst.Core.Models.PropData;

    /// <summary>
    /// Represents a property value converter for <see cref="InterestList"/>.
    /// </summary>
    [ConverterAlias(Constants.ExtendedProperties.InterestListConverterAlias, typeof(InterestList), 3)]
    public class InterestListValueConverter : PropertyValueConverterBase<InterestList>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InterestListValueConverter"/> class.
        /// </summary>
        /// <param name="prop">
        /// The <see cref="IExtendedProperty"/>.
        /// </param>
        public InterestListValueConverter(ExtendedProperty prop)
            : base(prop)
        {
        }
    }
}
namespace Catalyst.Core.ValueConverters
{
    using Catalyst.Core.Models.Domain;
    using Catalyst.Core.Models.PropData;

    /// <summary>
    /// Represents a property value converter for <see cref="Photo"/>.
    /// </summary>
    [ConverterAlias(Constants.ExtendedProperties.SocialLinksConverterAlias, typeof(SocialLinks), 2)]
    public class SocialLinksValueConverter : PropertyValueConverterBase<SocialLinks>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SocialLinksValueConverter"/> class.
        /// </summary>
        /// <param name="prop">
        /// The prop.
        /// </param>
        public SocialLinksValueConverter(ExtendedProperty prop)
            : base(prop)
        {
        }
    }
}
namespace Catalyst.Core.ValueConverters
{
    using Catalyst.Core.Models;
    using Catalyst.Core.Models.Domain;
    using Catalyst.Core.Models.PropData;

    /// <summary>
    /// Represents a property value converter for <see cref="Photo"/>.
    /// </summary>
    [ConverterAlias(Constants.ExtendedProperties.PhotoConverterAlias, typeof(Photo), 1)]
    public class PhotoValueConverter : PropertyValueConverterBase<Photo>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PhotoValueConverter"/> class.
        /// </summary>
        /// <param name="prop">
        /// The prop.
        /// </param>
        public PhotoValueConverter(ExtendedProperty prop)
            : base(prop)
        {
        }
    }
}
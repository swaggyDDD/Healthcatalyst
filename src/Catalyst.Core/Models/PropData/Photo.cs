namespace Catalyst.Core.Models.PropData
{
    /// <summary>
    /// Represents a photo.
    /// </summary>
    public class Photo : IPropertyValueModel
    {
        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        public string Src { get; set; }
    }
}
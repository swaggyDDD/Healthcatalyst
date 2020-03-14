namespace Catalyst.Core.Models.PropData
{
    /// <summary>
    /// Represents property data for social links.
    /// </summary>
    public class SocialLinks : IPropertyValueModel
    {
        /// <summary>
        /// Gets or sets the Facebook Url.
        /// </summary>
        public string Facebook { get; set; }

        /// <summary>
        /// Gets or sets the Twitter Url.
        /// </summary>
        public string Twitter { get; set; }

        /// <summary>
        /// Gets or sets the LinkedIn Url.
        /// </summary>
        public string LinkedIn { get; set; }

        /// <summary>
        /// Gets or sets the Google+ Url.
        /// </summary>
        public string GooglePlus { get; set; }

        /// <summary>
        /// Gets or sets the YouTube Url.
        /// </summary>
        public string YouTube { get; set; }

    }
}
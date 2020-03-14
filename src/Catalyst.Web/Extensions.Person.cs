namespace Catalyst.Web
{
    using Catalyst.Core;
    using Catalyst.Core.Models.Domain;
    using Catalyst.Core.Models.PropData;

    /// <summary>
    /// Extension methods for <see cref="Person"/>.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Gets the Url for the person.
        /// </summary>
        /// <param name="person">
        /// The person.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string Url(this IPerson person)
        {
            return person.Url("person");
        }

        //// Properties
        
        /// <summary>
        /// Checks if the <c>'Photo'</c> property exists.
        /// </summary>
        /// <param name="person">
        /// The person.
        /// </param>
        /// <returns>
        /// A value indicating whether or not the <c>'Photo'</c> property exists.
        /// </returns>
        public static bool HasPhoto(this IPerson person)
        {
            return person.HasExtendedProperty(Core.Constants.ExtendedProperties.PhotoConverterAlias);
        }

        /// <summary>
        /// Checks if the <c>'Interest List'</c> property exists.
        /// </summary>
        /// <param name="person">
        /// The person.
        /// </param>
        /// <returns>
        /// A value indicating whether or not the <c>'Interest List'</c> property exists.
        /// </returns>
        public static bool HasInterests(this IPerson person)
        {
            return person.HasExtendedProperty(Core.Constants.ExtendedProperties.InterestListConverterAlias);
        }

        /// <summary>
        /// Checks if the <c>'Social Links'</c> property exists.
        /// </summary>
        /// <param name="person">
        /// The person.
        /// </param>
        /// <returns>
        /// A value indicating whether or not the <c>'Social Links'</c> property exists.
        /// </returns>
        public static bool HasSocialLinks(this IPerson person)
        {
            return person.HasExtendedProperty(Core.Constants.ExtendedProperties.SocialLinksConverterAlias);
        }

        /// <summary>
        /// The photo url.
        /// </summary>
        /// <param name="person">
        /// The person.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string PhotoUrl(this IPerson person)
        {
            return person.HasPhoto()
                       ? person.GetPropertyValue<Photo>().Src
                       : string.Empty;
        }


        /// <summary>
        /// Gets the update date in local time.
        /// </summary>
        /// <param name="person">
        /// The person.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string UpdateDateLocal(this IPerson person)
        {
            return person.UpdateDate.ToLocalTime().ToString("g");
        }

    }
}
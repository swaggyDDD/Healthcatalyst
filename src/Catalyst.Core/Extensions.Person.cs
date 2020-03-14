namespace Catalyst.Core
{
    using System;
    using System.Linq;
    using System.Threading;

    using Catalyst.Core.DI;
    using Catalyst.Core.Models.Domain;
    using Catalyst.Core.Models.PropData;

    /// <summary>
    /// Extension methods related to <see cref="IEntity"/> models.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// The full name.
        /// </summary>
        /// <param name="person">
        /// The person.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string FullName(this IPerson person)
        {
            var textInfo = Thread.CurrentThread.CurrentCulture.TextInfo;
            return textInfo.ToTitleCase($"{person.FirstName} {person.LastName}");
        }

        /// <summary>
        /// Gets the Url for the person.
        /// </summary>
        /// <param name="person">
        /// The person.
        /// </param>
        /// <param name="baseRoute">
        /// The base Route.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string Url(this IPerson person, string baseRoute = "")
        {
            return $"{baseRoute.EnsureNotStartsOrEndsWith('/')}/{person.Slug}".EnsureStartsAndEndsWith('/');
        }

        /// <summary>
        /// Calculates the person's age.
        /// </summary>
        /// <param name="person">
        /// The person.
        /// </param>
        /// <returns>
        /// The age of the person.
        /// </returns>
        /// <seealso cref="http://stackoverflow.com/questions/9/how-do-i-calculate-someones-age-in-c"/>
        public static int Age(this IPerson person)
        {
            var now = DateTime.Today;
            var age = now.Year - person.Birthday.Year;
            if (person.Birthday > now.AddYears(-age)) age--;

            return age;
        }

        /// <summary>
        /// Checks if an extended property exists.
        /// </summary>
        /// <param name="person">
        /// The person.
        /// </param>
        /// <param name="converterAlias">
        /// The converter alias.
        /// </param>
        /// <returns>
        /// A value indicating whether or not the extended property exists.
        /// </returns>
        public static bool HasExtendedProperty(this IPerson person, string converterAlias)
        {
            return person.Properties.Any(x => x.ConverterAlias.Equals(converterAlias, System.StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// Gets a property by it's alias.
        /// </summary>
        /// <param name="person">
        /// The person.
        /// </param>
        /// <param name="converterAlias">
        /// The converter alias.
        /// </param>
        /// <returns>
        /// The <see cref="IExtendedProperty"/>.
        /// </returns>
        public static IExtendedProperty GetProperty(this IPerson person, string converterAlias)
        {
            return person.Properties.FirstOrDefault(x => x.ConverterAlias.Equals(converterAlias, System.StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// Gets the property value.
        /// </summary>
        /// <param name="person">
        /// The person.
        /// </param>
        /// <param name="converterAlias">
        /// The converter alias.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public static object GetValue(this IPerson person, string converterAlias)
        {
            var prop = person.GetProperty(converterAlias);
            return prop == null ? null : prop.Converter().GetValue();
        }

        /// <summary>
        /// Gets the property value.
        /// </summary>
        /// <param name="person">
        /// The person.
        /// </param>
        /// <param name="defaultToNewInstance">
        /// A value indicating whether or not do default to a new instance of the property value.
        /// </param>
        /// <typeparam name="TValue">
        /// The type of the value
        /// </typeparam>
        /// <returns>
        /// The <see cref="TValue"/>.
        /// </returns>
        public static TValue GetPropertyValue<TValue>(this IPerson person, bool defaultToNewInstance = false)
            where TValue : class, IPropertyValueModel, new()
        {
            var resolved = Active.ValueConverterRegister.ConverterMappings.FirstOrDefault(info => info.ValueType == typeof(TValue));
            if (resolved == null) return null;

            var prop = person.GetProperty(resolved.ConverterAlias);

            return prop != null ? 

                prop.GetPropertyValue<TValue>() : 
                
                defaultToNewInstance ? new TValue() : null;
        }

        /// <summary>
        /// Generates a slug for the person.
        /// </summary>
        /// <param name="person">
        /// The person.
        /// </param>
        /// <param name="attempt">
        /// The attempt.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        internal static string GenerateSlug(this IPerson person, int attempt = 0)
        {
            var baseSlug = $"{person.FirstName} {person.LastName}".ConvertToSlug().ToLowerInvariant();
            return attempt == 0 ? baseSlug : $"{baseSlug}-{attempt}";
        }
    }
}

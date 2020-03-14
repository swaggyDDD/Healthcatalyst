namespace Catalyst.Core.Configuration
{
    using System;
    using System.Reflection;

    using Semver;

    /// <summary>
    /// Represents the version of Catalyst.Core.
    /// </summary>
    /// <remarks>
    /// Some of the methods in this class are formatted specifically for build script modification.  
    /// </remarks>
    public class CatalystVersion
    {
        /// <summary>
        /// The version.
        /// </summary>
        private static readonly Version Version = new Version("1.0.0");

        /// <summary>
        /// Gets the current version.
        /// Version class with the specified major, minor, build (Patch), and revision numbers.
        /// </summary>
        // ReSharper disable once ConvertToAutoProperty
        public static Version Current
        {
            // ReSharper disable once ConvertPropertyToExpressionBody
            get { return Version; }
        }

        /// <summary>
        /// Gets the version comment (like beta or RC).
        /// </summary>
        /// <value>The version comment.</value>
        // ReSharper disable once StyleCop.SA1122
        public static string CurrentComment => "";

        /// <summary>
        /// Gets the assembly version.
        /// </summary>
        /// <remarks>
        /// Get the version of Catalyst.Core by looking at a class in that DLL
        /// Had to do it like this due to medium trust issues
        /// </remarks>
        /// <seealso cref="http://haacked.com/archive/2010/11/04/assembly-location-and-medium-trust.aspx"/>
        public static string AssemblyVersion
        {
            get
            {
                // ReSharper disable once ConvertPropertyToExpressionBody
                return new AssemblyName(typeof(CatalystVersion).Assembly.FullName).Version.ToString();
            }
        }

        /// <summary>
        /// Gets the semantic version.
        /// </summary>
        /// <returns>
        /// The <see cref="SemVersion"/>.
        /// </returns>
        public static SemVersion GetSemanticVersion()
        {
            return new SemVersion(
                Current.Major,
                Current.Minor,
                Current.Build,
                CurrentComment.IsNullOrWhiteSpace() ? null : CurrentComment,
                Current.Revision > 0 ? Current.Revision.ToInvariantString() : null);
        }
    }
}

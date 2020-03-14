namespace Catalyst.Web
{
    using System;
    using System.Web.WebPages;

    /// <summary>
    /// Extension methods for <see cref="WebPage"/>.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Allows for rending default content for Razor section
        /// </summary>
        /// <param name="webPage">
        /// The web page.
        /// </param>
        /// <param name="name">
        /// The name of the section.
        /// </param>
        /// <param name="defaultContents">
        /// The default contents.
        /// </param>
        /// <returns>
        /// The <see cref="HelperResult"/>.
        /// </returns>
        /// <seealso cref="http://haacked.com/archive/2011/03/05/defining-default-content-for-a-razor-layout-section.aspx/"/>
        public static HelperResult RenderSection(this WebPageBase webPage, string name, Func<dynamic, HelperResult> defaultContents)
        {
            return webPage.IsSectionDefined(name) ? 
                webPage.RenderSection(name) : 
                defaultContents(null);
        }
    }
}
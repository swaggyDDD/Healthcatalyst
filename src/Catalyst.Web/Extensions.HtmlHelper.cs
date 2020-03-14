namespace Catalyst.Web
{
    using System;
    using System.Web;
    using System.Web.Mvc;

    using Catalyst.Core;

    /// <summary>
    /// Extension methods for the <see cref="HtmlHelper"/>.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Reads markdown content as HTML.
        /// </summary>
        /// <param name="html">
        /// The <see cref="HtmlHelper"/>.
        /// </param>
        /// <param name="file">
        /// The file.
        /// </param>
        /// <param name="directory">
        /// The directory.
        /// </param>
        /// <returns>
        /// The <see cref="IHtmlString"/>.
        /// </returns>
        public static IHtmlString Markdown(this HtmlHelper html, string file, string directory = "")
        {
            var fileName = file.EnsureEndsWith(".md");

            if (directory.IsNullOrWhiteSpace()) directory = "~/App_Data/Markdown/";

            var markdownPath = $"{directory}{fileName}";

            var path = HttpContext.Current.Server.MapPath(markdownPath);

            if (System.IO.File.Exists(path))
            {
                var md = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath(markdownPath));
                if (!md.IsNullOrWhiteSpace())
                {
                    return MvcHtmlString.Create(CommonMark.CommonMarkConverter.Convert(md));
                }
            }

            return MvcHtmlString.Empty; 
        }

        /// <summary>
        /// Reads markdown content as HTML from a file named the same as the type referenced.
        /// </summary>
        /// <param name="html">
        /// The <see cref="HtmlHelper"/>.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="directory">
        /// The directory.
        /// </param>
        /// <returns>
        /// The <see cref="IHtmlString"/>.
        /// </returns>
        public static IHtmlString Markdown(this HtmlHelper html, Type type, string directory = "")
        {
            return html.Markdown(type.Name, directory);
        }

        /// <summary>
        /// Reads markdown content as HTML from a file named the same as the type referenced.
        /// </summary>
        /// <param name="html">
        /// The <see cref="HtmlHelper"/>.
        /// </param>
        /// <param name="directory">
        /// The directory.
        /// </param>
        /// <typeparam name="TModel">
        /// The type of the model
        /// </typeparam>
        /// <returns>
        /// The <see cref="IHtmlString"/>.
        /// </returns>
        public static IHtmlString Markdown<TModel>(this HtmlHelper html, string directory = "")
        {
            return html.Markdown(typeof(TModel), directory);
        }

        /// <summary>
        /// Utility inline if.
        /// </summary>
        /// <param name="html">
        /// The <see cref="HtmlHelper"/>.
        /// </param>
        /// <param name="condition">
        /// The condition.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="otherwise">
        /// The otherwise (else) value.
        /// </param>
        /// <returns>
        /// The <see cref="IHtmlString"/>.
        /// </returns>
        public static IHtmlString If(this HtmlHelper html, Func<bool> condition, string value, string otherwise = "")
        {
            return html.If(condition(), value, otherwise);
        }

        /// <summary>
        /// Utility inline if.
        /// </summary>
        /// <param name="html">
        /// The <see cref="HtmlHelper"/>.
        /// </param>
        /// <param name="condition">
        /// The condition.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="otherwise">
        /// The otherwise (else) value.
        /// </param>
        /// <returns>
        /// The <see cref="IHtmlString"/>.
        /// </returns>
        public static IHtmlString If(this HtmlHelper html, bool condition, string value, string otherwise = "")
        {
            return condition
               ? MvcHtmlString.Create(value)
               : otherwise.IsNullOrWhiteSpace() ? MvcHtmlString.Empty : MvcHtmlString.Create(otherwise);
        }

        /// <summary>
        /// Gets a thumbnail.
        /// </summary>
        /// <param name="html">
        /// The <see cref="HtmlHelper"/>.
        /// </param>
        /// <param name="src">
        /// The image source.
        /// </param>
        /// <param name="fallbackSrc">
        /// The fallback image source.
        /// </param>
        /// <param name="alt">
        /// The alternative text.
        /// </param>
        /// <param name="cssclass">
        /// The CSS class.
        /// </param>
        /// <param name="queryString">
        /// The query string.
        /// </param>
        /// <returns>
        /// The <see cref="IHtmlString"/>.
        /// </returns>
        public static IHtmlString Thumbnail(this HtmlHelper html, string src, string fallbackSrc = "", string alt = "", string cssclass = "", string queryString = "")
        {
            if (src.IsNullOrWhiteSpace() && fallbackSrc.IsNullOrWhiteSpace()) return MvcHtmlString.Empty;

            if (!queryString.IsNullOrWhiteSpace()) queryString = queryString.EnsureStartsWith('?');

            var imgSrc = !src.IsNullOrWhiteSpace() ? src : fallbackSrc;

            var css = $"class=\"{cssclass}\"".Trim();

            return MvcHtmlString.Create($"<img src=\"{imgSrc}{queryString}\" alt=\"{HttpUtility.HtmlEncode(alt)}\" {css} />");
        }
    }
}
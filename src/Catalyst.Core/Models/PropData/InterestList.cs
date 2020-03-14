namespace Catalyst.Core.Models.PropData
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Gets a list of interests.
    /// </summary>
    public class InterestList : IPropertyValueModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InterestList"/> class.
        /// </summary>
        public InterestList()
        {
            Values = Enumerable.Empty<Interest>();
        }

        /// <summary>
        /// Gets or sets the values.
        /// </summary>
        public IEnumerable<Interest> Values { get; set; }
    }

    /// <summary>
    /// Represents an interest.
    /// </summary>
    // ReSharper disable once StyleCop.SA1402
    public class Interest
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the url.
        /// </summary>
        public string Url { get; set; }
    }
}
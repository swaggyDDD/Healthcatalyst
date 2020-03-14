namespace Catalyst.Web.Models.Dashboard
{
    using System.Collections.Generic;

    using Catalyst.Core.Models.Domain;

    /// <summary>
    /// Model for people listing dashboards.
    /// </summary>
    public class PeopleListing : DashboardItemBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PeopleListing"/> class.
        /// </summary>
        /// <param name="title">
        /// The title.
        /// </param>
        public PeopleListing(string title)
        {
            Title = title;
        }

        /// <inheritdoc />
        public override string Title { get; }

        /// <summary>
        /// Gets or sets the list of people.
        /// </summary>
        public IEnumerable<Person> People { get; set; }

        /// <summary>
        /// Gets or sets the number of total people.
        /// </summary>
        public long TotalPeople { get; set; }

        /// <summary>
        /// Gets or sets the query term.
        /// </summary>
        public string QueryTerm { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to show the manage button.
        /// </summary>
        public bool ShowManage { get; set; } = false;
    }
}
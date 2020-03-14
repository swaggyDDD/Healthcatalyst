namespace Catalyst.Web.Models.Dashboard
{
    using System.Collections.Generic;

    /// <summary>
    /// Model for dashboard item that lists countries associated with people tracked.
    /// </summary>
    public class CountriesSnapshot : DashboardItemBase
    {
        /// <inheritdoc />                                     
        public override string Title => "Top 5 Countries";

        /// <summary>
        /// Gets or sets the country metrics.
        /// </summary>
        public IEnumerable<CountryMetric> Metrics { get; set; }
    }
}
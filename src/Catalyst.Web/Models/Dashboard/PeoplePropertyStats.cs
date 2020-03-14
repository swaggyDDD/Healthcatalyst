namespace Catalyst.Web.Models.Dashboard
{
    using System.Collections.Generic;

    /// <summary>
    /// Model for dashboard item that shows how many people have extended properties.
    /// </summary>
    public class PeoplePropertyStats : DashboardItemBase
    {
        /// <summary>
        /// The title.
        /// </summary>
        public override string Title => "Properties Added to People";

        /// <summary>
        /// Gets or sets the metrics.
        /// </summary>
        public IEnumerable<Metric> Metrics { get; set; }
    }
}
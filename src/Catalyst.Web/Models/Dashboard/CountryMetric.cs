namespace Catalyst.Web.Models.Dashboard
{
    /// <summary>
    /// Represents a Country Metric.
    /// </summary>
    // ReSharper disable once StyleCop.SA1402
    public class CountryMetric : Metric
    {
        /// <summary>
        /// Gets or sets the country code.
        /// </summary>
        public string CountryCode { get; set; }
    }
}
namespace Catalyst.Web.Models.Dashboard
{
    using System;

    using Catalyst.Core.Models.Domain;
    using Catalyst.Core.Models.PropData;

    /// <summary>
    /// Model for dashboard item that shows a randomly watched person.
    /// </summary>
    public class RandomWatch : DashboardItemBase
    {
        /// <summary>
        /// The title.
        /// </summary>
        public override string Title => "Random Watched Person";

        /// <summary>
        /// Gets or sets the person.
        /// </summary>
        public IPerson Person { get; set; }
    }
}
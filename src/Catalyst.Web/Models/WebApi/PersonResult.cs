namespace Catalyst.Web.Models.WebApi
{
    using System;

    using Newtonsoft.Json;

    /// <summary>
    /// Represents a search result.
    /// </summary>
    public class PersonResult
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the url.
        /// </summary>
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
    }
}
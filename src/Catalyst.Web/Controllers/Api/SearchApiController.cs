namespace Catalyst.Web.Controllers.Api
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;

    using Catalyst.Core;
    using Catalyst.Core.DI;
    using Catalyst.Core.Logging;
    using Catalyst.Core.Services;
    using Catalyst.Web.Models.WebApi;

    /// <summary>
    /// Responsible for the search requests.
    /// </summary>
    public class SearchApiController : ApiController
    {
        /// <summary>
        /// The <see cref="ILogger"/>.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// The <see cref="IServiceContext"/>.
        /// </summary>
        private readonly IServiceContext _services;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchApiController"/> class.
        /// </summary>
        public SearchApiController()
            : this(Active.Logger, Active.Services) 
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchApiController"/> class.
        /// </summary>
        /// <param name="logger">
        /// The <see cref="ILogger"/>.
        /// </param>
        /// <param name="services">
        /// The <see cref="IServiceContext"/>.
        /// </param>
        public SearchApiController(ILogger logger, IServiceContext services)
        {
            if (logger == null) throw new ArgumentNullException(nameof(logger));
            if (services == null) throw new ArgumentNullException(nameof(services));

            _logger = logger;
            _services = services;
        }

        /// <summary>
        /// Gets all people.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable{SearchResult}"/>.
        /// </returns>
        [HttpGet]
        public IEnumerable<PersonResult> GetAll()
        {
            var service = _services.Person;
            return service.GetAll().Select(p => new PersonResult { Id = p.Id, Name = p.FullName(), Url = p.Url(Web.Constants.PersonRoute) }).ToArray();
        }

        /// <summary>
        /// Searches for a name.
        /// </summary>
        /// <param name="q">
        /// The name to search.
        /// </param>
        /// <returns>
        /// A collection of search results.
        /// </returns>
        [HttpGet]
        public IEnumerable<PersonResult> SearchByName(string q)
        {
            var service = _services.Person;

            return service.SearchNames(q).Select(p => new PersonResult { Id = p.Id, Name = p.FullName(), Url = p.Url(Web.Constants.PersonRoute) }).ToArray();
        } 
    }
}

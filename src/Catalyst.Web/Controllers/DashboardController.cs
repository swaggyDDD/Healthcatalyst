namespace Catalyst.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using Catalyst.Core;
    using Catalyst.Core.Controllers;
    using Catalyst.Core.Mvc;
    using Catalyst.Web.Models.Dashboard;

    /// <summary>
    /// A controller for rendering dashboard components.
    /// </summary>
    public class DashboardController : CatalystControllerBase
    {
        /// <summary>
        /// Renders a placeholder for an asynchronous dashboard item.
        /// </summary>
        /// <param name="apiRouteId">
        /// The API route id.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [ChildActionOnly]
        public ActionResult Placeholder(string apiRouteId)
        {
            var model = new AsyncPlaceholder { AjaxRouteAlias = apiRouteId };

            return PartialView(model);
        }

        /// <summary>
        /// Renders a RichText dashboard
        /// </summary>
        /// <param name="title">
        /// The dashboard item title.
        /// </param>
        /// <param name="file">
        /// The markdown file name.
        /// </param>
        /// <param name="notes">
        /// The dashboard item notes.
        /// </param>
        /// <param name="directory">
        /// The directory.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [ChildActionOnly]
        public ActionResult RichText(string title, string file, string notes = "", string directory = "")
        {
            var model = new RichText(title)
            {
                MarkdownFile = file,
                Notes = notes,
                Directory = directory
            };

            return PartialView("RichText", model);
        }

        /// <summary>
        /// Renders the recently updated people.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [ChildActionOnly]
        public ActionResult RecentlyAdded()
        {
            var model = new PeopleListing("Recent Updates")
            {
                People = Services.Person.GetRecentlyUpdated(),
                ShowManage = true,
                TotalPeople = Services.Person.Count()
            };

            return PartialView("PeopleList", model);
        }

        /// <summary>
        /// Renders the people list.
        /// </summary>
        /// <param name="q">
        /// The optional query term.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [ChildActionOnly]
        public ActionResult PeopleList(string q = "")
        {
            var title = q.IsNullOrWhiteSpace() ? "All People" : $"Showing results for '{q}'";
            var people = q.IsNullOrWhiteSpace()
                             ? Services.Person.GetAll().OrderBy(x => x.FirstName).ToArray()
                             : Services.Person.SearchNames(q, int.MaxValue).ToArray();

            var model = new PeopleListing(title)
            {
                People = people,
                QueryTerm = q,
                TotalPeople = people.Count()
            };

            return PartialView(model);
        }

        // - ASYNCHRONOUS ----------------

        /// <summary>
        /// Responsible for rendering "Country Snap Shot" dashboard item.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        [CheckAjaxRequest]
        public ActionResult CountriesSnapshot()
        {

            var countries = Services.AddressService.GetAssociatedCountries();

            var metrics = new List<CountryMetric>();
            foreach (var c in countries)
            {
                metrics.Add(
                    new CountryMetric
                        {
                            CountryCode = c.Code,
                            Name = c.Name,
                            Count = Services.AddressService.CountPeopleAddressInCountry(c.Code)
                        });
            }

            var model = new CountriesSnapshot
                {
                    AjaxRouteAlias = Web.Constants.AjaxRouteAliases.CompanySnapshot,
                    Metrics = metrics.OrderByDescending(x => x.Count).Take(5)
                };

            return PartialView(model);
        }

        /// <summary>
        /// Responsible for rendering the "People Property Stats" dashboard item.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        [CheckAjaxRequest]
        public ActionResult PeoplePropertyStats()
        {
            var metrics = new List<Metric>();

            var photo = new Metric
            {
                Name = "Photo",
                Count = Services.ExtendedPropertyService.CountPeopleUsing(Constants.ExtendedProperties.PhotoConverterAlias)
            };

            metrics.Add(photo);

            // interests
            var interests = new Metric
            {
                Name = "Interests",
                Count = Services.ExtendedPropertyService.CountPeopleUsing(Constants.ExtendedProperties.InterestListConverterAlias)
            };

            metrics.Add(interests);

            var sociallinks = new Metric
            {
                Name = "Social Links",
                Count = Services.ExtendedPropertyService.CountPeopleUsing(Constants.ExtendedProperties.SocialLinksConverterAlias)
            };

            metrics.Add(sociallinks);

            var model = new PeoplePropertyStats
                {
                    AjaxRouteAlias = Web.Constants.AjaxRouteAliases.PeoplePropertyStats,
                    Metrics = metrics
                };

            return PartialView(model);
        }

        /// <summary>
        /// Responsible for rendering the "Random Last Tweet" dashboard item.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        [CheckAjaxRequest]
        public ActionResult RandomWatched()
        {
            var watched = Services.Person.GetWatched().OrderBy(x => Guid.NewGuid()).FirstOrDefault();

            if (watched == null) return PartialView("_Empty");
            
            var model = new RandomWatch
            {
                AjaxRouteAlias = Web.Constants.AjaxRouteAliases.RandomWatched,
                Person = watched
            };
            return PartialView(model);
        }
    }
}
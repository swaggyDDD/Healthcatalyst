// ReSharper disable StyleCop.SA1600
namespace Catalyst.Web
{
    /// <summary>
    /// Constants for Catalyst.Web.
    /// </summary>
    public static class Constants
    {
        public const string HomeRoute = "/";
        public const string PeopleRoute = "/people/";
        public const string PersonRoute = "/person/";

        public const string PersonEmptyPhoto = "/media/placeholders/person-placeholder.png";

        public static class AjaxRouteAliases
        {
            public const string CompanySnapshot = "countrymetrics";
            public const string PeoplePropertyStats = "peopleprops";
            public const string RandomWatched = "randomwatched";

            // editors
            public const string AddPerson = "addperson";
            public const string UpdatePerson = "updateperson";
            public const string InterestList = "interestlist";
            public const string SocialLinks = "sociallinks";
            public const string Photo = "photo";
            public const string Address = "address";

            // only used to get the UI to display the spinner e.g. used for templating
            public const string DevDoNothing = "donothing"; 
        }
    }
}
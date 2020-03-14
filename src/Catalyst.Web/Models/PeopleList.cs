namespace Catalyst.Web.Models
{
    using Catalyst.Web.Models.Shared;

    /// <summary>
    /// Represents the people list page view model.
    /// </summary>
    public class PeopleList : ViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PeopleList"/> class.
        /// </summary>
        public PeopleList()
        {
            Meta = new Meta
            {
                PageTitle = "People List - People Problem",
                Description = "Displays a list of all people that have been saved to the database"
            };
        }

        /// <summary>
        /// Gets or sets the query term.
        /// </summary>
        public string QueryTerm { get; set; }
    }
}
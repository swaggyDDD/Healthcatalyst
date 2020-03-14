namespace Catalyst.Web.Models
{
    using Catalyst.Core.Models.Domain;
    using Catalyst.Web.Models.Shared;

    /// <summary>
    /// Represents a person detail view model.
    /// </summary>
    public class PersonDetail : ViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PersonDetail"/> class.
        /// </summary>
        public PersonDetail()
        {
            Meta = new Meta
                {
                    PageTitle = "Person Details - People Problem",
                    Description = "Provides access to all records associated with a person"
                };
        }

        /// <summary>
        /// Gets or sets the person.
        /// </summary>
        public Person Person { get; set; }
    }
}
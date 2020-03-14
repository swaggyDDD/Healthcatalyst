namespace Catalyst.Web.Areas.Editors.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using Catalyst.Core.Controllers;
    using Catalyst.Core.Models.Domain;
    using Catalyst.Core.Mvc;
    using Catalyst.Web.Areas.Editors.Models;

    /// <summary>
    /// Controller responsible for editing person address information.
    /// </summary>
    public class AddressEditorController : CatalystControllerBase, IEditorControllerBase<AddressEditor> 
    {
        /// <inheritdoc />
        [HttpGet]
        [CheckAjaxRequest]
        public ActionResult Editor(Guid id)
        {
            var person = Services.Person.Get(id);
            return View(BuildModel(person));
        }

        /// <inheritdoc />
        [HttpPost]
        public ActionResult Save(AddressEditor model)
        {
            var person = Services.Person.Get(model.PersonId);

            Address address;
            if (person.Addresses.Any())
            {
                address = person.Addresses.First();
            }
            else
            {
                address = new Address();
                person.Addresses.Add(address);
            }

            address.Name = model.Name;
            address.Address1 = model.Address1;
            address.Address2 = model.Address2;
            address.Locality = model.Locality;
            address.Region = model.Region;
            address.CountryCode = model.CountryCode;
            address.PostalCode = model.PostalCode;

            if (address.Id.Equals(Guid.Empty)) person.Addresses.Add(address);

            Services.Person.Save(person);

            return Redirect(model.ReturnUrl);
        }

        /// <summary>
        /// Builds the model.
        /// </summary>
        /// <param name="person">
        /// The person.
        /// </param>
        /// <returns>
        /// The <see cref="AddressEditor"/>.
        /// </returns>
        private AddressEditor BuildModel(Person person)
        {
            var model = new AddressEditor("Address")
                {
                    PersonId = person.Id,
                    ReturnUrl = person.Url()                    
                };

            var adr = person.Addresses.FirstOrDefault();

            if (adr != null)
            {
                model.AddressId = adr.Id;
                model.Name = adr.Name;
                model.Address1 = adr.Address1;
                model.Address2 = adr.Address2;
                model.Locality = adr.Locality;
                model.Region = adr.Region;
                model.CountryCode = adr.CountryCode;
                model.PostalCode = adr.PostalCode;
            }

            var countries = Services
                .AddressService
                .GetAllCountries()
                .Select(c => new SelectListItem
                    {
                        Value = c.Code,
                        Text = c.Name,
                        Selected = c.Code.Equals(model.CountryCode, StringComparison.InvariantCultureIgnoreCase)
                    })
                .ToList();

            model.Countries = countries;

            return model;
        }
    }
}
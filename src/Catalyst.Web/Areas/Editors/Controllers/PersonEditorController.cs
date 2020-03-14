namespace Catalyst.Web.Areas.Editors.Controllers
{
    using System;
    using System.Web.Mvc;

    using Catalyst.Core;
    using Catalyst.Core.Controllers;
    using Catalyst.Core.Models.Domain;
    using Catalyst.Core.Mvc;
    using Catalyst.Web.Areas.Editors.Models;

    /// <summary>
    /// Editor controller responsible for <see cref="IPerson"/>.
    /// </summary>
    public class PersonEditorController : CatalystControllerBase
    {
        /// <summary>
        /// Responsible for rendering the editor form.
        /// </summary>
        /// <param name="id">
        /// The person id.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        [CheckAjaxRequest]
        public ActionResult Editor(Guid? id = null)
        {
            var person = id.HasValue ?
                Services.Person.Get(id.Value) :
                null;

            var model = person != null
                ? new PersonEditor($"Update {person.FullName()}")
                {
                    PersonId = person.Id,
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    Birthday =
                                person.Birthday.ToShortDateString(),
                    Watch = person.Watch
                }
                : new PersonEditor("Add a Person") { ReturnUrl = Web.Constants.PeopleRoute };

            return View("Editor", model);
        }

        /// <summary>
        /// Saves a new person.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(PersonEditor model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("Editor", model);
            }

            DateTime birthday;
            if (DateTime.TryParse(model.Birthday, out birthday))
            {
                Person person;
                if (model.PersonId.Equals(Guid.Empty))
                {
                    // new Person
                    person = Services.Person.Create(model.FirstName, model.LastName, birthday);
                }
                else
                {
                    person = Services.Person.Get(model.PersonId);
                    person.FirstName = model.FirstName;
                    person.LastName = model.LastName;
                    person.Birthday = birthday;
                }

                person.Watch = model.Watch;

                Services.Person.Save(person);

                return Redirect(person.Url(Web.Constants.PersonRoute));
            }
            
            return Redirect(model.ReturnUrl);
        }

        /// <summary>
        /// Deletes a person.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="r">
        /// The redirect url.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        public ActionResult Delete(Guid id, string r = "/")
        {
            var person = Services.Person.Get(id);

            if (person == null)
            {
                var nullRef = new NullReferenceException("Person record was null in Delete");
                Logger.Error<PersonEditorController>("Person not found", nullRef);
                throw nullRef;
            }

            Services.Person.Delete(person);

            return Redirect(r);
        }

        /// <summary>
        /// Toggles whether or not a person is watched.
        /// </summary>
        /// <param name="id">
        /// The person's id.
        /// </param>
        /// <param name="r">
        /// The current page route.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        public ActionResult ToggleWatched(Guid id, string r)
        {
            var person = Services.Person.Get(id);

            if (person == null)
            {
                var nullRef = new NullReferenceException("Person record was null in ToggleWatched");
                Logger.Error<PersonEditorController>("Person not found", nullRef);
                throw nullRef;
            }

            person.Watch = !person.Watch;
            Services.Person.Save(person);

            return Redirect(r);
        }

        ///// <summary>
        ///// Responsible for rendering a form to add a person.
        ///// </summary>
        ///// <returns>
        ///// The <see cref="ActionResult"/>.
        ///// </returns>
        //[HttpGet]
        //[CheckAjaxRequest]
        //public ActionResult AddPerson()
        //{
        //    var model = new PersonEditor("Add a Person")
        //        {
        //            ReturnUrl = Web.Constants.PeopleRoute
        //        };

        //    return View("AddEditPerson", model);
        //}

        ///// <summary>
        ///// Responsible for rendering a form to edit a person.
        ///// </summary>
        ///// <param name="person">
        ///// The <see cref="IPerson"/>.
        ///// </param>
        ///// <returns>
        ///// The <see cref="ActionResult"/>.
        ///// </returns>
        //[ChildActionOnly]
        //public ActionResult EditPerson(IPerson person)
        //{
        //    var model = new PersonEditor($"Update {person.FullName()}")
        //        {
        //            PersonId = person.Id,
        //            FirstName = person.FirstName,
        //            LastName = person.LastName,
        //            Birthday = person.Birthday.ToShortDateString(),
        //            Watch = person.Watch
        //        };

        //    return View("AddEditPerson", model);
        //}
    }
}
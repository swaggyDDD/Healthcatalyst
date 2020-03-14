namespace Catalyst.Web.Areas.Editors.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using Catalyst.Core;
    using Catalyst.Core.Controllers;
    using Catalyst.Core.Models.Domain;
    using Catalyst.Core.Models.PropData;
    using Catalyst.Core.Mvc;
    using Catalyst.Web.Areas.Editors.Models;

    /// <summary>
    /// The interest editor controller.
    /// </summary>
    public class InterestEditorController : EditorControllerBase<InterestList, InterestListEditor>
    {
        /// <inheritdoc />
        [HttpGet]
        [CheckAjaxRequest]
        public override ActionResult Editor(Guid id)
        {
            var person = Services.Person.Get(id);
            return View(BuildModel(person));
        }


        /// <inheritdoc />
        [HttpPost]
        public override ActionResult Save(InterestListEditor model)
        {
            var person = Services.Person.Get(model.PersonId);

            if (ModelState.IsValid)
            {
                var interest = new Interest { Title = model.InterestName, Url = model.Url };
                var interests = person.GetPropertyValue<InterestList>(true);

                // Update the property
                // TODO - this needs to be handled in the property itself (should not 
                // need to work so hard here)
                var list = new List<Interest>();
                list.AddRange(interests.Values);
                list.Add(interest);
                interests.Values = list;

                var prop = person.GetProperty(this.ConverterMapping.ConverterAlias);

                if (prop == null)
                {
                    prop = new ExtendedProperty
                    {
                        ConverterAlias = this.ConverterMapping.ConverterAlias
                    };

                    person.Properties.Add((ExtendedProperty)prop);
                }
                           
                prop.SetValue(interests);

                Services.Person.Save(person, true);

            }

            if (Request.IsAjaxRequest())
            {
                return View("Editor", BuildModel(person));
            }

            return Redirect(model.ReturnUrl);
        }

        /// <summary>
        /// Removes an interest.
        /// </summary>
        /// <param name="id">
        /// The person id.
        /// </param>
        /// <param name="idx">
        /// The index of the item to be removed.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Remove(Guid id, int idx)
        {
            try
            {
                var person = Services.Person.Get(id);
                var interests = person.GetPropertyValue<InterestList>();
                var list = interests.Values.ToList();
                list.RemoveAt(idx);
                interests.Values = list;

                var prop = person.GetProperty(ConverterMapping.ConverterAlias);
                prop.SetValue(interests);

                Services.Person.Save(person, true);

                return View("Editor", BuildModel(person));

            }
            catch (Exception ex)
            {
                Logger.WarnWithException<InterestEditorController>("Failed to remove interest", ex);
                return Redirect("/");
            }
        }

        /// <summary>
        /// Builds a model.
        /// </summary>
        /// <param name="person">
        /// The person.
        /// </param>
        /// <returns>
        /// The <see cref="InterestListEditor"/>.
        /// </returns>
        private InterestListEditor BuildModel(Person person)
        {
            return new InterestListEditor("Interests")
            {
                PersonId = person.Id,
                InterestList =
                person.GetPropertyValue<InterestList>(true),
                ReturnUrl = person.Url(Web.Constants.PersonRoute)
            };
        }
    }
}
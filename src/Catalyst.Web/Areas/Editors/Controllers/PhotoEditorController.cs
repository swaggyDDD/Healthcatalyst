namespace Catalyst.Web.Areas.Editors.Controllers
{
    using System;
    using System.IO;
    using System.Web.Mvc;

    using Catalyst.Core;
    using Catalyst.Core.Controllers;
    using Catalyst.Core.Models.Domain;
    using Catalyst.Core.Models.PropData;
    using Catalyst.Core.Mvc;
    using Catalyst.Web.Areas.Editors.Models;

    /// <summary>
    /// Property editor controller responsible for <see cref="Photo"/>.
    /// </summary>
    public class PhotoEditorController : EditorControllerBase<Photo, PhotoEditor>
    {
        /// <summary>
        /// Place to store files.
        /// </summary>
        private const string MediaPath = "~/media/store/";

        /// <inheritdoc />
        [HttpGet]
        [CheckAjaxRequest]
        public override ActionResult Editor(Guid id)
        {
            var person = Services.Person.Get(id);

            var model = new PhotoEditor("Photo")
            {
                PersonId = person.Id,
                PhotoUrl = person.PhotoUrl(),
                ReturnUrl = person.Url(Web.Constants.PersonRoute)
            };

            return View(model);
        }

        /// <inheritdoc />
        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Save(PhotoEditor model)
        {
            var file = model.PostedFile;

            var person = Services.Person.Get(model.PersonId);

            //// only allow image types
            if (file != null && file.IsImage())
            {
                EnsureSavePath();

                var extension = Path.GetExtension(file.FileName);

                // rename the file
                var filename = $"{person.Id}{extension}";
                
                var savePath = Path.Combine(this.Server.MapPath(MediaPath), filename);
                
                file.SaveAs(savePath);

                // upload complete at this point.  associate with person
                var photo = person.GetPropertyValue<Photo>(true);
                photo.Path = savePath;

                var src = $"{MediaPath.EnsureNotEndsWith('~')}{filename}";
                photo.Src = src.EnsureNotStartsWith('~').EnsureForwardSlashes().ToLowerInvariant();

                var prop = person.GetProperty(ConverterMapping.ConverterAlias);
                if (prop == null)
                {
                    prop = new ExtendedProperty { ConverterAlias = ConverterMapping.ConverterAlias };
                    person.Properties.Add((ExtendedProperty)prop);
                }

                prop.SetValue(photo);

                Services.Person.Save(person, true);

            }
            else
            {
                var ex = new Exception("File uploaded was null or not an image type");
                Logger.Error<PhotoEditorController>("Failed to upload media", ex);
                throw ex;
            }

            return Redirect(model.ReturnUrl);
        }

        /// <summary>
        /// Ensures the store directory is created.
        /// </summary>
        private void EnsureSavePath()
        {
            var media = Server.MapPath("~/media");
            var store = Server.MapPath(MediaPath);
            if (!Directory.Exists(media)) Directory.CreateDirectory(media);
            if (!Directory.Exists(store)) Directory.CreateDirectory(store);
        }
    }
}
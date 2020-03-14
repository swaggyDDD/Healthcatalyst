namespace Catalyst.Core.Controllers
{
    using System;
    using System.Web.Mvc;

    /// <summary>
    /// Represents a simple model editor controller.
    /// </summary>
    /// <typeparam name="TEditorModel">
    /// The type of the editor model
    /// </typeparam>
    public interface IEditorControllerBase<in TEditorModel> where TEditorModel : class, new()
    {
        /// <summary>
        /// Responsible for rendering the editor.
        /// </summary>
        /// <param name="id">
        /// The person id.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        ActionResult Editor(Guid id);

        /// <summary>
        /// Responsible for saving the property value.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        ActionResult Save(TEditorModel model);
    }
}
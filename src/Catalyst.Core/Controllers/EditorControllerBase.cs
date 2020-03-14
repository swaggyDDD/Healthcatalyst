namespace Catalyst.Core.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using Catalyst.Core.DI;
    using Catalyst.Core.Models.PropData;
    using Catalyst.Core.ValueConverters;

    /// <summary>
    /// The property editor controller base.
    /// </summary>
    /// <typeparam name="TValue">
    /// The type of the value
    /// </typeparam>
    /// <typeparam name="TEditorModel">
    /// The type of the editor model
    /// </typeparam>
    public abstract class EditorControllerBase<TValue, TEditorModel> : CatalystControllerBase, IEditorControllerBase<TEditorModel> where TValue : class, IPropertyValueModel, new()
        where TEditorModel : class, new()
    {
        /// <summary>
        /// Gets the <see cref="IConverterMappingInfo"/>.
        /// </summary>
        public IConverterMappingInfo ConverterMapping => Active.ConverterMappings.FirstOrDefault(x => x.ValueType == typeof(TValue));

        /// <summary>
        /// Responsible for rendering the editor.
        /// </summary>
        /// <param name="id">
        /// The person id.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public abstract ActionResult Editor(Guid id);

        /// <summary>
        /// Responsible for saving the property value.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public abstract ActionResult Save(TEditorModel model);
    }
}
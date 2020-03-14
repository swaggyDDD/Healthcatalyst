namespace Catalyst.Web.Controllers
{
    using System.Web;
    using System.Web.Mvc;

    using Catalyst.Core;
    using Catalyst.Core.Controllers;
    using Catalyst.Web.Models;
    using Catalyst.Web.Models.Dashboard;
    using Catalyst.Web.Models.Shared;

    /// <summary>
    /// A base controller responsible for constructing view models.
    /// </summary>
    public abstract class ViewModelControllerBase : CatalystControllerBase
    {
        /// <summary>
        /// Instantiates a view model.
        /// </summary>
        /// <param name="contentTitle">
        /// The title for the content box.
        /// </param>
        /// <typeparam name="TModel">
        /// The type of the model
        /// </typeparam>
        /// <returns>
        /// The <see cref="TModel"/>.
        /// </returns>
        protected virtual TModel GetViewModel<TModel>(string contentTitle = "") where TModel : ViewModel, new()
        {
            var current = Request.Url != null ? Request.RawUrl : string.Empty;

            return new TModel
            {
                CurrentTab = new NavTab { IsCurrent = true, Target = "_self", Url = current }
            };
        }


    }
}
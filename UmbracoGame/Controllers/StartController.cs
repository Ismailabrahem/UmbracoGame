using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Cms.Web.Common.PublishedModels;
using UmbracoGame.Models.ViewModels;
using Umbraco.Cms.Core;

namespace UmbracoGame.Controllers
{
    public class StartController : RenderController
    {
        private readonly IUmbracoContextAccessor _umbracoContextAccessor;
        private readonly IPublishedContentQuery _publishedContentQuery;

        public StartController(ILogger<RenderController> logger, ICompositeViewEngine compositeViewEngine, IUmbracoContextAccessor umbracoContextAccessor) : base(logger, compositeViewEngine, umbracoContextAccessor)
        {
            _umbracoContextAccessor = umbracoContextAccessor;
        }

        public override IActionResult Index()
        {
            var startPage = CurrentPage as Start;

            if (startPage != null)
            {
                var model = new StartPageViewModel(startPage, _umbracoContextAccessor);
                return View("start", model);
            }

            return null;
        }
    }
}

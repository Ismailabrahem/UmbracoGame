using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Cms.Web.Common.PublishedModels;
using UmbracoGame.Models;
using UmbracoGame.Models.ViewModels;

namespace UmbracoGame.Controllers
{
    public class AboutUsController : RenderController
    {
        private readonly IUmbracoContextAccessor _umbracoContextAccessor;

        public AboutUsController(ILogger<RenderController> logger, ICompositeViewEngine compositeViewEngine, IUmbracoContextAccessor umbracoContextAccessor) : base(logger, compositeViewEngine, umbracoContextAccessor)
        {
            _umbracoContextAccessor = umbracoContextAccessor;
        }

        public override IActionResult Index()
        {
            var aboutusPage = CurrentPage as AboutUs;

            if (aboutusPage != null)
            {
                var model = new AboutUsPageViewModel(aboutusPage, _umbracoContextAccessor);

                return View("aboutUs", model);
            }

            return null;
        }
    }
}

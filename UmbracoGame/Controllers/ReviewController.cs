using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using UmbracoGame.Models.ViewModels;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace UmbracoGame.Controllers
{
    public class ReviewController : RenderController
    {
        private readonly IUmbracoContextAccessor _umbracoContextAccessor;

        public ReviewController(ILogger<RenderController> logger, ICompositeViewEngine compositeViewEngine, IUmbracoContextAccessor umbracoContextAccessor) : base(logger, compositeViewEngine, umbracoContextAccessor)
        {
            _umbracoContextAccessor = umbracoContextAccessor;
        }

        public override IActionResult Index()
        {
            var query = HttpContext.Request.Query["query"];

            var reviewPage = CurrentPage as Review;

            if (reviewPage != null)
            {
                var model = new ReviewPageViewModel(reviewPage, _umbracoContextAccessor);

                return View("review", model);
            }

            return null;
        }
    }
}

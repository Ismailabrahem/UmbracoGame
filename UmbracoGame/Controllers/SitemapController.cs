using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Cms.Web.Common.PublishedModels;
using UmbracoGame.Models.ViewModels;
using UmbracoGame.Business.Services.Interfaces;
using UmbracoGame.Business.Services;

namespace UmbracoGame.Controllers
{
    public class SitemapController : RenderController
    {
        private readonly IUmbracoContextAccessor _umbracoContextAccessor;
        private readonly ISitemapService _sitemapService;

        public SitemapController(ILogger<RenderController> logger, ISitemapService sitemapService, ICompositeViewEngine compositeViewEngine, IUmbracoContextAccessor umbracoContextAccessor) : base(logger, compositeViewEngine, umbracoContextAccessor)
        {
            _umbracoContextAccessor = umbracoContextAccessor;
            _sitemapService = sitemapService;
        }

        public override IActionResult Index()
        {
            var sitemap= CurrentPage as Sitemap;

            if (sitemap != null)
            {
                var model = new SitemapPageViewModel(sitemap, _umbracoContextAccessor);
                model.Pages = _sitemapService.Pages();
                return View("sitemap", model);
            }

            return null;
        }
    }
}

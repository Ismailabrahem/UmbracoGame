namespace UmbracoGame.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using global::UmbracoGame.Business.Services.Interfaces;
    using Umbraco.Cms.Web.Website.Controllers;
    using Umbraco.Cms.Core.Web;
    using Umbraco.Cms.Core.Cache;
    using Umbraco.Cms.Core.Logging;
    using Umbraco.Cms.Core.Routing;
    using Umbraco.Cms.Core.Services;
    using Umbraco.Cms.Infrastructure.Persistence;
    using Umbraco.Cms.Web.Common.PublishedModels;

    namespace UmbracoGame.Controllers
    {
        public class ReviewSurfaceController : SurfaceController
        {
            private readonly IPetaPocoService _petaPocoService;
            private readonly IUmbracoContextAccessor _umbracoContextAccessor;

            public ReviewSurfaceController(IPetaPocoService petaPocoService, IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory, ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider) : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)

            {
                _petaPocoService = petaPocoService;
                _umbracoContextAccessor = umbracoContextAccessor;
            }

            [HttpPost]
            public IActionResult AddReview(string GameId, string Name, string Comment, int Rating)
            {
                if (!string.IsNullOrEmpty(GameId) && !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Comment))
                {
                    var review = new Models.Review
                    {
                        GameId = GameId,
                        Name = Name,
                        Comment = Comment,
                        Rating = Rating,
                        Date = DateTime.UtcNow  // Ensure Date is set correctly
                    };

                    _petaPocoService.AddReview(review);

                    // Return a success response
                    return Json(new { success = true, message = "Review added successfully!" });
                }

                // Return an error response if validation fails
                return Json(new { success = false, message = "Invalid input data." });
            }


            [HttpGet]
            public JsonResult GetReviews(string gameId)
            {
                var reviews = _petaPocoService.GetReviews(gameId);
                return Json(reviews);
            }
        }
    }
}
    



using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Common.PublishedModels;
using Umbraco.Cms.Web.Website.ActionResults;
using Umbraco.Cms.Web.Website.Controllers;
using UmbracoGame.Models;
using UmbracoGame.Business.Services.Interfaces;
using UmbracoGame.Models.ViewModels;

namespace UmbracoGame.Controllers
{
    public class GameSurfaceController : SurfaceController
    {
        private readonly IUmbracoContextAccessor _umbracoContextAccessor;
        private readonly IGameService _gameService; // Inject the game service
        private readonly IVariationContextAccessor _variationContextAccessor;

        public GameSurfaceController(IUmbracoContextAccessor umbracoContextAccessor, IGameService gameService, IUmbracoDatabaseFactory databaseFactory, ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider, IVariationContextAccessor variationContextAccessor) : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
        {
            _umbracoContextAccessor = umbracoContextAccessor;
            _gameService = gameService;
            _variationContextAccessor = variationContextAccessor;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GameDetails(string id)
        {
            var game = await _gameService.GetGameByIdAsync(id);

            if (game == null)
            {
                return NotFound();
            }

            // Fetch screenshots and trailers
            game.Screenshots = await _gameService.GetGameScreenshotsAsync(id);
            game.Movies = await _gameService.GetGameTrailersAsync(id, game.Name);

            var page = CurrentPage as GamePage;
            var model = new GamePageViewModel(page)
            {
                Game = game
            };

            return View("gamePage", model);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AddGame(GameDetails model)
        {
            if (model != null && !string.IsNullOrEmpty(model.Id))
            {
                model.Culture = _variationContextAccessor.VariationContext.Culture;

                if (!_gameService.GameExists(model.Id))
                {
                    _gameService.AddGame(model, "GameId");
                    return Json(new { success = true, message = "Game added successfully!" });
                }
                else
                {
                    return Json(new { success = false, message = "Game already exists!" });
                }
            }

            return Json(new { success = false, message = "Invalid game details!" });
        }

    }
}



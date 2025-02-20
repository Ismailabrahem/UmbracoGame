using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Umbraco.Cms.Web.Common.PublishedModels;
using UmbracoGame.Models.ViewModels;
using UmbracoGame.Business.Services.Interfaces;

public class GameController : RenderController
{
    private readonly IGameService _gameService;
    private IUmbracoContextAccessor _umbracoContextAccessor;

    public GameController(
        ILogger<RenderController> logger,
        ICompositeViewEngine compositeViewEngine,
        IUmbracoContextAccessor umbracoContextAccessor,
        IGameService gameService
    ) : base(logger, compositeViewEngine, umbracoContextAccessor)
    {
        _gameService = gameService;
    }

    [HttpGet]
    public async Task<IActionResult> Details(string id, string slug)
    {
        var game = await _gameService.GetGameByIdAsync(id);
        if (game == null || GenerateSlug(game.Name) != slug)
        {
            return NotFound();
        }

        game.Movies = await _gameService.GetGameTrailersAsync(id, game.Name);

        // Ensure CurrentPage is a GamePage
        if (CurrentPage is not GamePage page)
        {
            return NotFound();
        }

        var model = new GamePageViewModel(page)
        {
            Game = game
        };

        return View("gamepage", model);
    }

    private string GenerateSlug(string name)
    {
        return name.ToLower().Replace(" ", "-");
    }


}

//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.ViewEngines;
//using Microsoft.Extensions.Logging;
//using System.Threading.Tasks;
//using Umbraco.Cms.Core.Web;
//using Umbraco.Cms.Web.Common.Controllers;
//using Umbraco.Cms.Web.Common.PublishedModels;
//using UmbracoGame.Business.Services.Interfaces;
//using UmbracoGame.Models.ViewModels;

//namespace UmbracoGame.Controllers
//{
//    public class GamePageController : RenderController
//    {
//        private readonly IGameService _gameService;

//        public GamePageController(
//            ILogger<GamePageController> logger,
//            ICompositeViewEngine compositeViewEngine,
//            IUmbracoContextAccessor umbracoContextAccessor,
//            IGameService gameService)
//            : base(logger, compositeViewEngine, umbracoContextAccessor)
//        {
//            _gameService = gameService;
//        }

//        // ✅ Properly override the Index method
//        public override async Task<IActionResult> Index()
//        {
//            if (CurrentPage is not GamePage page)
//            {
//                return NotFound();
//            }

//            var game = await _gameService.GetGameByIdAsync(page.Key.ToString());

//            var model = new GamePageViewModel(page)
//            {
//                Game = game
//            };

//            return CurrentTemplate(model); // ✅ Ensures Umbraco renders the correct template
//        }
//    }
//}

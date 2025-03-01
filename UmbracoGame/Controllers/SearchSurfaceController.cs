﻿using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Common.PublishedModels;
using Umbraco.Cms.Web.Website.Controllers;
using UmbracoGame.Business.Services.Interfaces;
using UmbracoGame.Models.ViewModels;

namespace UmbracoGame.Controllers
{
    public class SearchSurfaceController : SurfaceController
    {
        private readonly IGameService _gameService;
        private readonly IUmbracoContextAccessor _umbracoContextAccessor;

        public SearchSurfaceController(IGameService gameService, IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory, ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider) : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
        {
            _gameService = gameService;
            _umbracoContextAccessor = umbracoContextAccessor;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchGame(string query)
        {
            var searchPage = CurrentPage as Search;

            var model = new SearchPageViewModel(searchPage, _umbracoContextAccessor)
            {
                Games = await _gameService.GetGamesWithDetailsAsync(query)
            };

            return View("search", model);
        }
    }
}

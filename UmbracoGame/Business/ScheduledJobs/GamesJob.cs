using Hangfire.Console;
using Hangfire.Server;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.PublishedModels;
using Umbraco.Cms.Infrastructure.Scoping;

namespace UmbracoGame.Business.ScheduledJobs
{
    public class GamesJob : IGamesJob
    {
        private readonly IUmbracoContextFactory _umbracoContextFactory;
        private readonly IContentService _contentService;
        private readonly IScopeProvider _scopeProvider;
        private readonly ILocalizationService _localizationService;

        public GamesJob(
            IUmbracoContextFactory umbracoContextFactory,
            IContentService contentService,
            ILocalizationService localizationService,
            IScopeProvider scopeProvider)
        {
            _umbracoContextFactory = umbracoContextFactory;
            _contentService = contentService;
            _localizationService = localizationService;
            _scopeProvider = scopeProvider;
        }

        public void RemoveGames(PerformContext context)
        {
            try
            {
                context.WriteLine("Starting RemoveGames job...");

                // Create a standalone Umbraco context
                using (var umbracoContextReference = _umbracoContextFactory.EnsureUmbracoContext())
                {
                    var content = umbracoContextReference.UmbracoContext.Content;

                    // Find the Settings page
                    var settingsPage = content.GetAtRoot().DescendantsOrSelf<Settings>().FirstOrDefault();
                    if (settingsPage == null)
                    {
                        context.WriteLine("Settings page not found.");
                        return;
                    }

                    context.WriteLine($"Settings page found: {settingsPage.Name} (ID: {settingsPage.Id})");

                    // Find the GamesContainer
                    var gamesContainer = settingsPage.GamesContainer;
                    if (gamesContainer == null)
                    {
                        context.WriteLine("GamesContainer not found.");
                        return;
                    }

                    context.WriteLine($"GamesContainer found: {gamesContainer.Name} (ID: {gamesContainer.Id})");

                    // Get all available languages in Umbraco
                    var allLanguages = _localizationService.GetAllLanguages().Select(l => l.IsoCode).ToList();

                    foreach (var language in allLanguages)
                    {
                        context.WriteLine($"Processing language: {language}");

                        var games = gamesContainer.Children<Game>(language).ToList();

                        if (!games.Any())
                        {
                            context.WriteLine($"No games found for language: {language}");
                            continue;
                        }

                        context.WriteLine($"Number of games found in {language}: {games.Count}");


                        // Iterate through the games and process them
                        foreach (var game in games)
                        {
                            context.WriteLine($"Processing game: {game.Name} (ID: {game.Id})");

                            // Get the IContent object for the game
                            var gameContent = _contentService.GetById(game.Id);
                            if (gameContent != null)
                            {
                                // Remove the game from Umbraco
                                _contentService.Delete(gameContent);
                                context.WriteLine($"Game {game.Name} removed successfully.");
                            }
                            else
                            {
                                context.WriteLine($"Game {game.Name} not found in the content service.");
                            }
                        }

                        context.WriteLine("All games processed successfully.");
                    }
                }
            }
            catch (Exception ex)
            {
                context.WriteLine($"Error in RemoveGames job: {ex.Message}");
                context.WriteLine($"Stack Trace: {ex.StackTrace}");
            }
        }
    }
}
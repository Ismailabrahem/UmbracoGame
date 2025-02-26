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

        public GamesJob(
            IUmbracoContextFactory umbracoContextFactory,
            IContentService contentService,
            IScopeProvider scopeProvider)
        {
            _umbracoContextFactory = umbracoContextFactory;
            _contentService = contentService;
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

                    // Get all child nodes (games) under the GamesContainer
                    var games = gamesContainer.Children<Game>("en-US").ToList();
                    if (games == null || !games.Any())
                    {
                        context.WriteLine("No games found in the GamesContainer.");
                        return;
                    }

                    context.WriteLine($"Number of games found: {games.Count}");

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
            catch (Exception ex)
            {
                context.WriteLine($"Error in RemoveGames job: {ex.Message}");
                context.WriteLine($"Stack Trace: {ex.StackTrace}");
            }
        }
    }
}
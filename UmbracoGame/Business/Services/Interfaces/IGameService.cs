using UmbracoGame.Models;

namespace UmbracoGame.Business.Services.Interfaces
{
    public interface IGameService
    {
        Task<List<GameDetails>> GetGamesWithDetailsAsync(string query);
        Task<GameDetails> GetGameByIdAsync(string Id);
        Task<List<Screenshot>> GetGameScreenshotsAsync(string Id);
        Task<List<Movie>> GetGameTrailersAsync(string Id, string name);
        bool GameExists(string id);
        void AddGame(GameDetails item, string gameIdAlias);

    }
}

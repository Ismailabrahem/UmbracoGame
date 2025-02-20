using Newtonsoft.Json;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using UmbracoGame.Business.Services.Interfaces;
using UmbracoGame.Models;

namespace UmbracoGame.Business
{
    public class GameService : IGameService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<GameService> _logger;
        private readonly string _youtubeApiKey;
        private IUmbracoContextAccessor _umbracoContextAccessor;
        private readonly IContentService _contentService;

        public GameService(HttpClient httpClient, ILogger<GameService> logger, IConfiguration configuration, IUmbracoContextAccessor umbracoContextAccessor, IContentService contentService)
        {
            _httpClient = httpClient;
            _logger = logger;
            _youtubeApiKey = configuration["YouTube:ApiKey"];
            _umbracoContextAccessor = umbracoContextAccessor;
            _contentService = contentService;
        }

        public async Task<List<GameDetails>> GetGamesWithDetailsAsync(string query)
        {
            var games = new List<GameDetails>();

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.rawg.io/api/games?key=2f1cdd6ea8db42419d774d7d9a979ea8&search={query}");
                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var searchResult = JsonConvert.DeserializeObject<Root>(json);

                    if (searchResult != null && searchResult.Results != null)
                    {
                        games = searchResult.Results;
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error fetching movies with details");
            }

            return games;
        }


        
        public async Task<GameDetails> GetGameByIdAsync(string id)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.rawg.io/api/games/{id}?key=2f1cdd6ea8db42419d774d7d9a979ea8");
                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var game = JsonConvert.DeserializeObject<GameDetails>(json);
                    return game;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error fetching game details for ID {id}");
            }

            return null; // Return null if there was an error
        }




        public async Task<List<Screenshot>> GetGameScreenshotsAsync(string Id)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.rawg.io/api/games/{Id}/screenshots?key=2f1cdd6ea8db42419d774d7d9a979ea8");
                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ApiResponse<List<Screenshot>>>(json);
                    return result?.Results ?? new List<Screenshot>();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error fetching screenshots for game ID {Id}");
            }

            return new List<Screenshot>();
        }

        public async Task<List<Movie>> GetGameTrailersAsync(string Id, string gameName)
        {
            // Step 1: Try fetching trailers from RAWG API
            var rawgTrailers = await GetRawgTrailersAsync(Id);

            if (rawgTrailers.Any())
            {
                return rawgTrailers;
            }

            // Step 2: Fallback to YouTube API if no trailers are found
            var youtubeTrailers = await GetYouTubeTrailersAsync(gameName);

            return youtubeTrailers;
        }

        private async Task<List<Movie>> GetRawgTrailersAsync(string Id)
        {
            try
            {
                var apiUrl = $"https://api.rawg.io/api/games/{Id}/movies?key=YOUR_RAWG_API_KEY";
                var response = await _httpClient.GetStringAsync(apiUrl);
                var result = JsonConvert.DeserializeObject<ApiResponse<List<Movie>>>(response);

                return result?.Results ?? new List<Movie>();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error fetching trailers from RAWG for game ID {Id}");
                return new List<Movie>();
            }
        }

        private async Task<List<Movie>> GetYouTubeTrailersAsync(string gameName)
        {
            try
            {
                var searchQuery = Uri.EscapeDataString($"{gameName} official trailer");
                var apiUrl = $"https://www.googleapis.com/youtube/v3/search?part=snippet&q={searchQuery}&key={_youtubeApiKey}&maxResults=5&type=video";

                var response = await _httpClient.GetStringAsync(apiUrl);
                var result = JsonConvert.DeserializeObject<YouTubeApiResponse>(response);

                var trailers = result.Items
                    .Select(item => new Movie
                    {
                        Name = item.Snippet.Title,
                        Preview = item.Snippet.Thumbnails.Default.Url,
                        Data = new MovieData
                        {
                            HighQuality = $"https://www.youtube.com/watch?v={item.Id.VideoId}"
                        }
                    })
                    .ToList();

                return trailers;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error fetching trailers from YouTube for game {gameName}");
                return new List<Movie>();
            }
        }

        // Helper class for API responses
        public class ApiResponse<T>
        {
            [JsonProperty("results")]
            public T Results { get; set; }

        }
    }
}

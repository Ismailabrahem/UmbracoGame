using Newtonsoft.Json;

namespace UmbracoGame.Models
{
    public class Root
    {
        [JsonProperty("results")]
        public List<GameDetails> Results { get; set; }
    }
}

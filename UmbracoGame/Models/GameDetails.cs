using Newtonsoft.Json;
using System.Collections.Generic;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace UmbracoGame.Models
{
    public class GameDetails
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description_raw")]
        public string Description { get; set; }

        [JsonProperty("metacritic")]
        public int? Metacritic { get; set; }

        [JsonProperty("released")]
        public string Released { get; set; }

        [JsonProperty("background_image")]
        public string BackgroundImage { get; set; }

        [JsonProperty("rating")]
        public double Rating { get; set; }

        [JsonProperty("playtime")]
        public int Playtime { get; set; }

        [JsonProperty("platforms")]
        public List<Platform> Platforms { get; set; }

        [JsonProperty("genres")]
        public List<Genre> Genres { get; set; }

        [JsonProperty("esrb_rating")]
        public EsrbRating EsrbRating { get; set; }

        [JsonProperty("developers")]
        public List<Developer> Developers { get; set; }

        [JsonProperty("publishers")]
        public List<Publisher> Publishers { get; set; }

        [JsonProperty("website")]
        public string Website { get; set; }

        [JsonProperty("stores")]
        public List<Store> Stores { get; set; }

        [JsonProperty("screenshots")]
        public List<Screenshot> Screenshots { get; set; }

        [JsonProperty("movies")]
        public List<Movie> Movies { get; set; }

        public string Culture { get; set; }

    }

    public class Platform
    {
        [JsonProperty("platform")]
        public PlatformDetails PlatformDetails { get; set; }
    }

    public class PlatformDetails
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Genre
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class EsrbRating
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Developer
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Publisher
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Store
    {
        [JsonProperty("store")]
        public StoreDetails StoreDetails { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }

    public class StoreDetails
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Screenshot
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }
    }

    public class Movie
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("preview")]
        public string Preview { get; set; }

        [JsonProperty("data")]
        public MovieData Data { get; set; }
    }

    public class MovieData
    {
        [JsonProperty("480")]
        public string LowQuality { get; set; }

        [JsonProperty("max")]
        public string HighQuality { get; set; }
    }

    // YouTube API Response Classes
    public class YouTubeApiResponse
    {
        [JsonProperty("items")]
        public List<YouTubeVideo> Items { get; set; }
    }

    public class YouTubeVideo
    {
        [JsonProperty("id")]
        public YouTubeVideoId Id { get; set; }

        [JsonProperty("snippet")]
        public YouTubeSnippet Snippet { get; set; }
    }

    public class YouTubeVideoId
    {
        [JsonProperty("videoId")]
        public string VideoId { get; set; }
    }

    public class YouTubeSnippet
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("thumbnails")]
        public YouTubeThumbnails Thumbnails { get; set; }
    }

    public class YouTubeThumbnails
    {
        [JsonProperty("default")]
        public YouTubeThumbnail Default { get; set; }
    }

    public class YouTubeThumbnail
    {
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
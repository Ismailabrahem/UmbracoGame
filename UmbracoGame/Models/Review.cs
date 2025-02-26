namespace UmbracoGame.Models
{
    public class Review
    {
        public int id { get; set; }
        public string GameId { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Comment { get; set; } = string.Empty;

        public int Rating { get; set; }

        public DateTime Date { get; set; }
    }
}

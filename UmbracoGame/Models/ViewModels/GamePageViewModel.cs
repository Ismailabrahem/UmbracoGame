using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace UmbracoGame.Models.ViewModels
{
    public class GamePageViewModel
    {
        public GamePage Content { get; }
        public GameDetails Game { get; set; }

        public GamePageViewModel(GamePage content)
        {
            Content = content;
        }
    }
}

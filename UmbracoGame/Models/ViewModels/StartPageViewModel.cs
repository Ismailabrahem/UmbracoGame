using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace UmbracoGame.Models.ViewModels
{
    public class StartPageViewModel : BasePageModel<Start>
    {
        public IEnumerable<SlideshowPage> Slides { get; }
        public StartPageViewModel(Start content, IUmbracoContextAccessor umbracoContextAccessor) : base(content, umbracoContextAccessor)
        {
            var slideshowContainer = content.Children<SlideshowContainer>()?.FirstOrDefault();

            // Extract SlideshowPage items
            Slides = slideshowContainer?.Children<SlideshowPage>() ?? Enumerable.Empty<SlideshowPage>();
        }


    }
}

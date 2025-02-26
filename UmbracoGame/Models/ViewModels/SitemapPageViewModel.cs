using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace UmbracoGame.Models.ViewModels
{
    public class SitemapPageViewModel : BasePageModel<Sitemap>
    {
        public List<IPublishedContent> Pages { get; set; }
        public SitemapPageViewModel(Sitemap content, IUmbracoContextAccessor umbracoContextAccessor) : base(content, umbracoContextAccessor)
        {
            Pages = new List<IPublishedContent>(); // Ensures it is never null
        }
    }
}

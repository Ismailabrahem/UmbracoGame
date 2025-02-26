using Umbraco.Cms.Core.Models.PublishedContent;

namespace UmbracoGame.Business.Services.Interfaces
{
    public interface ISitemapService
    {
        List<IPublishedContent> Pages();
    }
}

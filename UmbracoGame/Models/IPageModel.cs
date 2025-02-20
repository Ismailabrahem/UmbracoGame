using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace UmbracoGame.Interfaces
{
    public interface IPageModel : IPublishedContent
    {
        IPublishedContent Content { get; }
        Start StartPage { get; }
    }
}
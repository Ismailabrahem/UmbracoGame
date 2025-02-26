using Umbraco.Cms.Core.Web;

namespace UmbracoGame.Models.ViewModels
{
    public class ReviewPageViewModel : BasePageModel<Umbraco.Cms.Web.Common.PublishedModels.Review>
    {
        public ReviewPageViewModel(Umbraco.Cms.Web.Common.PublishedModels.Review content, IUmbracoContextAccessor umbracoContextAccessor) : base(content, umbracoContextAccessor)
        {
        }
    }
}

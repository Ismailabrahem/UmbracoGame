using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace UmbracoGame.Models.ViewModels
{
    public class AboutUsPageViewModel : BasePageModel<AboutUs>
    {
        public AboutUsPageViewModel(AboutUs content, IUmbracoContextAccessor umbracoContextAccessor) : base(content, umbracoContextAccessor)
        {
        }
    }
}

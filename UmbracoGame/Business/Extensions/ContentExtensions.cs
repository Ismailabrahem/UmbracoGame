using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;

namespace UmbracoGame.Business.Extensions
{
    public static class ContentExtensions
    {
        public static string GetFullUrl(this IPublishedContent content, string language)
        {
            // Resolve IUmbracoContextAccessor from the current service provider
            var umbracoContextAccessor = StaticServiceProvider.Instance.GetService<IUmbracoContextAccessor>();

            if (umbracoContextAccessor?.TryGetUmbracoContext(out var umbracoContext) ?? false)
            {
                // Ensure the culture exists for the content
                if (content.Cultures.ContainsKey(language))
                {
                    // Generate the full URL for the specified language variant
                    return content.Url(mode: UrlMode.Absolute, culture: language);
                }
            }

            return string.Empty;
        }
    }
}

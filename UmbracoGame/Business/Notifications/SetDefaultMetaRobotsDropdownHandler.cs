using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Core.Services;

namespace UmbracoGame.Business.Notifications
{
    public class SetDefaultMetaRobotsDropdownHandler : INotificationHandler<ContentSavingNotification>
    {
        private readonly IVariationContextAccessor _variationContextAccessor;
        private readonly IContentService _contentService;

        public SetDefaultMetaRobotsDropdownHandler(IVariationContextAccessor variationContextAccessor, IContentService contentService)
        {
            _variationContextAccessor = variationContextAccessor;
            _contentService = contentService;
        }

        public void Handle(ContentSavingNotification notification)
        {
            foreach (var content in notification.SavedEntities)
            {
                // Check if this content is newly created (Id is 0) and has the 'metaRobots' property
                if (content.Id == 0 && content.HasProperty("metaRobots"))
                {
                    // Get the current culture from the VariationContextAccessor
                    var currentCulture = _variationContextAccessor.VariationContext.Culture;

                    // Check if the property is null or empty for the current culture and set the default value
                    var metaRobots = content.GetValue<string>("metaRobots", currentCulture);

                    if (string.IsNullOrWhiteSpace(metaRobots))
                    {
                        // Assign the default value for the 'metaRobots' property directly on the content object
                        content.SetValue("metaRobots", "INDEX, FOLLOW", currentCulture);
                    }
                }
            }
        }
    }
}

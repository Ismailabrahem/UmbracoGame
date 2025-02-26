using Hangfire.Server;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace UmbracoGame.Business.ScheduledJobs
{
    public interface IGamesJob
    {
        void RemoveGames(PerformContext context = null);
    }

}

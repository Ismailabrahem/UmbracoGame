using Hangfire;
using Umbraco.Cms.Core.Composing;
using UmbracoGame.Business.ScheduledJobs;

namespace UmbracoGame.Business.Composers
{
    public class ScheduledJobsComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            RecurringJob.AddOrUpdate<IGamesJob>(
                "Remove games",
                x => x.RemoveGames(null),
                Cron.Daily);
        }
    }
}

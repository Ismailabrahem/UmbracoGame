using UmbracoGame.Models;

namespace UmbracoGame.Business.Services.Interfaces
{
    public interface IPetaPocoService
    {
        void EnsureReviewsTableCreated();

        void AddReview(Review review);

        List<Review> GetReviews(string gameId);

        void TruncateTable(string name);
    }
}

using Microsoft.Data.SqlClient;
using NPoco;
using UmbracoGame.Business.Services.Interfaces;
using UmbracoGame.Models;

namespace UmbracoGame.Business.Services
{
    public class PetaPocoService : IPetaPocoService
    {
        private readonly string _connectionString;

        public PetaPocoService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void EnsureReviewsTableCreated()
        {
            using (var db = new Database(_connectionString, DatabaseType.SqlServer2012, SqlClientFactory.Instance))
            {
                var sql = @"
            IF NOT EXISTS (
                SELECT * FROM sys.tables WHERE name = 'Reviews' AND schema_id = SCHEMA_ID('dbo')
            )
            BEGIN
                CREATE TABLE dbo.Reviews
                (
                    Id INT IDENTITY (1,1) PRIMARY KEY,
                    GameId NVARCHAR(50) NOT NULL,
                    Name NVARCHAR(50) NOT NULL,
                    Comment NVARCHAR(MAX) NOT NULL,
                    Rating INT NOT NULL,
                    Date DATETIME NOT NULL
                ) 
                ON [PRIMARY] 
                TEXTIMAGE_ON [PRIMARY];
            END";

                db.Execute(sql);
            }
        }

        public void AddReview(Review review)
        {
            using (var db = new Database(_connectionString, DatabaseType.SqlServer2012, SqlClientFactory.Instance))
            {
                var sql = @"
                INSERT INTO dbo.Reviews (GameId, Name, Comment, Rating, Date)
                VALUES (@0, @1, @2, @3, @4)";

                db.Execute(sql, review.GameId, review.Name, review.Comment, review.Rating, review.Date);
            }
        }

        public List<Review> GetReviews(string gameId)
        {
            using (var db = new Database(_connectionString, DatabaseType.SqlServer2012, SqlClientFactory.Instance))
            {
                var sql = @"
        SELECT Id, GameId, Name, Comment, Rating, Date
        FROM dbo.Reviews
        WHERE GameId = @0
        ORDER BY Date DESC";

                var reviews = db.Fetch<Review>(sql, gameId);

                // Debugging: Print to Console
                foreach (var review in reviews)
                {
                    Console.WriteLine($"Review Loaded: {review.Name}, {review.Comment}, {review.Rating}, {review.Date}");
                }

                return reviews;
            }
        }


        public void TruncateTable(string name)
        {
            using (var db = new Database(_connectionString, DatabaseType.SqlServer2012, SqlClientFactory.Instance))
            {
                var sql = "TRUNCATE TABLE dbo.Reviews";
                db.Execute(sql);
            }
        }
    }

}

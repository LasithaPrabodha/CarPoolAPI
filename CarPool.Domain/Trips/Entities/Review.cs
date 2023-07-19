using CarPool.Domain.Common;
using CarPool.Domain.Users;

namespace CarPool.Domain.Trips;

public class Review : Entity<ReviewId>
{
    public UserId UserId { get; private set; }
    public int Rating { get; private set; }
    public string ReviewText { get; private set; }

    private Review() { }

    private Review(ReviewId reviewId, UserId userId, int rating, string reviewText)
        : base(reviewId)
    {
        UserId = userId;
        Rating = rating;
        ReviewText = reviewText;

    }

    public static Review Create(UserId userId, int rating, string reviewText)
    {
        return new(
            ReviewId.CreateUnique(),
            userId,
            rating,
            reviewText
        );
    }
}


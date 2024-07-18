using Tourism.Core.Entities;

namespace Tourism.Core.Repositories.Contract
{
    public interface IReviewRepository
    {
        Task<Review> AddReviewAsync(Review review);
        Task<Review> UpdateReviewAsync(int id, Review review);
        Task DeleteReviewAsync(int id);
        Task<Review> GetReviewByIdAsync(int Id);
        Task<IEnumerable<Review>> GetAllReviewByPlaceIdAsync(int PlaceId);
    }
}

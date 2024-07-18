
using Microsoft.EntityFrameworkCore;
using Tourism.Core.Entities;
using Tourism.Core.Repositories.Contract;
using Tourism.Repository.Data;

namespace Tourism.Repository.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly TourismContext _context;


        public ReviewRepository(TourismContext context)
        {
            _context = context;
        }

        public async Task<Review> AddReviewAsync(Review review)
        {

            var findByUserIdAndPlaceId = await _context.Reviews.FirstOrDefaultAsync(r => r.UserId == review.UserId && r.PlaceId == review.PlaceId);

            if (findByUserIdAndPlaceId != null)
            {
                throw new InvalidOperationException("User has already reviewed this place.");
            }
            else
            {
                Place place = await _context.Places.FindAsync(review.PlaceId);
                if (place == null)
                {
                    throw new ArgumentException($"This Place Doesn't Exist");
                }
                ApplicationUser finduser = await _context.Users.FindAsync(review.UserId);
                if (finduser == null)
                {
                    throw new ArgumentException($"This User Doesn't Exist");
                }
                if (review.Rating < 0 || review.Rating > 5)
                {
                    throw new ArgumentOutOfRangeException("Rating starts with one and ends with five");
                }
                _context.Reviews.Add(review);
                await _context.SaveChangesAsync();
            }

            return review;
        }

        public async Task<Review> UpdateReviewAsync(int id, Review review)
        {
            if (id != review.Id)
            {
                throw new ArgumentException($"Review Id {id} Doesn't Exist");
            }
            else
            {
                if (review.Rating < 0 || review.Rating > 5)
                {
                    throw new ArgumentOutOfRangeException("Rating starts with one and ends with five");
                }
                _context.Reviews.Update(review);
                await _context.SaveChangesAsync();
            }
            return review;
        }

        public async Task DeleteReviewAsync(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review != null)
            {
                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("This Review Not Exist");
            }
        }

        public async Task<Review> GetReviewByIdAsync(int id)
        {

            return await _context.Reviews.FirstOrDefaultAsync(R => R.Id == id);
        }

        public async Task<IEnumerable<Review>> GetAllReviewByPlaceIdAsync(int PlaceId)
        {
            return await _context.Reviews
                .Include(p => p.Place)
                .Include(u => u.User)
                .Where(p => p.PlaceId == PlaceId).Select(r => new Review
                {
                    Message = r.Message,
                    Rating = r.Rating,
                    PlaceId = r.PlaceId,
                    UserId = r.UserId,
                    Time = r.Time,
                    CreatedDate = r.CreatedDate,
                    ModifiedDate = r.ModifiedDate,
                })
                .ToListAsync();
        }
    }
}

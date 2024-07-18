using Tourism.Core.Entities;
using Tourism.Core.Repositories.Contract;
using Tourism.Repository.Data;
using Tourism.Repository.Repository;

namespace Tourism.Repository
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : BaseEntity
    {
        public IGenericRepository<T> generic { get; set; }
        public IReviewRepository review { get; set; }
        public IChangePassword changePassword { get; set; }
        public IPlace_TripRepository placeTrip { get; set; }
        private readonly TourismContext _context;


        public UnitOfWork(TourismContext context)
        {
            _context = context;
            generic = new GenericRepository<T>(_context);
            review = new ReviewRepository(_context);
            placeTrip = new Place_TripRepository(_context);
            changePassword = new ChangePassword(_context);
        }

        public int Complet()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

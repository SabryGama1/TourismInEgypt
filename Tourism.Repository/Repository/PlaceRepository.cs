using Microsoft.EntityFrameworkCore;
using Tourism.Core.Entities;
using Tourism.Core.Repositories.Contract;
using Tourism.Repository.Data;

namespace Tourism.Repository.Repository
{
    public class PlaceRepository : GenericRepository<Place>, IPlaceRepository
    {
        private readonly TourismContext context;

        public PlaceRepository(TourismContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<Place> GetAsync(int id)
        {
            return await context.Places.Include(c => c.City).Include(c => c.Category).Include(c => c.Photos).Include(c => c.Reviews).ThenInclude(u => u.User).FirstAsync(p => p.Id == id);

        }
        public async Task<IEnumerable<Place>> GetAllAsync()
        {

            var places = await context.Places.Include("City").Include("Category").Include("Photos").ToListAsync();
            return places;
        }
        //public async Task<List<PlacePhotos>> GetAllPhotoBySpecIdAsync(int id)
        //{
        //    return await context.PlacePhotos.Where(p=>p.PlaceId == id).ToListAsync();
        //}
    }
}

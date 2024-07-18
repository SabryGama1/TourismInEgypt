using Microsoft.EntityFrameworkCore;
using Tourism.Core.Entities;
using Tourism.Core.Repositories.Contract;
using Tourism.Repository.Data;

namespace Tourism.Repository.Repository
{
    public class CityRepository : GenericRepository<City>, ICityRepository
    {
        private readonly TourismContext context;

        public CityRepository(TourismContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<City> GetAsync(int id)
        {
            return await context.Cities.Include(c => c.Places).ThenInclude(c => c.Photos).Include(c => c.CityPhotos).Include(c => c.Places).ThenInclude(c => c.Category).FirstAsync(p => p.Id == id);

        }
        public async Task<IEnumerable<City>> GetAllAsync()
        {

            var cities = await context.Cities.Include(c => c.Places).ThenInclude(c => c.Photos).Include(c => c.CityPhotos).Include(c => c.Places).ThenInclude(c => c.Category).ToListAsync();
            return cities;
        }
        //public async Task<List<CityPhotos>> GetAllPhotoBySpecIdAsync(int id)
        //{
        //    return await context.CityPhotos.Where(c => c.CityId == id).ToListAsync();
        //}
    }
}

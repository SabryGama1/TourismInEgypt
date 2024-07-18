using Microsoft.EntityFrameworkCore;
using Tourism.Core.Entities;
using Tourism.Core.Repositories.Contract;
using Tourism.Repository.Data;

namespace Tourism.Repository.Repository
{
    public class Place_TripRepository : GenericRepository<Place_Trip>, IPlace_TripRepository
    {

        private readonly TourismContext _context;

        public Place_TripRepository(TourismContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Place_Trip>> GetAllWithPlaceAndTrip()
        {
            return await _context.Place_Trips.Include("Place").Include("Trip").ToListAsync();
        }


        public async Task<Place_Trip> GetOneWithPlaceAndTrip(int id)
        {
            return await _context.Place_Trips.Include("Place").Include("Trip").FirstAsync(x => x.Id == id);
        }
        public async Task<IEnumerable<Place_Trip>> GetTripWithplaces(int id)
        {
            return await _context.Place_Trips.Where(x => x.TripId == id).Include("Trip").ToListAsync();
        }

        public async Task<Place_Trip> ForUnique(int valueofplace, int trip)
        {
            return await _context.Place_Trips.FirstAsync(x => x.PlaceId == valueofplace && x.TripId == trip);
        }

    }
}

using Microsoft.EntityFrameworkCore;
using Tourism.Core.Entities;
using Tourism.Core.Repositories.Contract;
using Tourism.Repository.Data;

namespace Tourism.Repository.Repository
{
    public class TripRepository : GenericRepository<Trip>, ITripRepository
    {
        private readonly TourismContext context;

        public TripRepository(TourismContext context) : base(context)
        {
            this.context = context;
        }



        public async Task<IEnumerable<Place_Trip>> GetplacesByIdofTrip(int id)
        {
            return await context.Place_Trips.Where(x => x.TripId == id).Include("Place").ToListAsync();

        }
    }
}

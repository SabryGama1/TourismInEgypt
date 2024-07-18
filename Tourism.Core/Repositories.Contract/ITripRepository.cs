using Tourism.Core.Entities;

namespace Tourism.Core.Repositories.Contract
{
    public interface ITripRepository
    {
        public Task<IEnumerable<Place_Trip>> GetplacesByIdofTrip(int id);
    }
}

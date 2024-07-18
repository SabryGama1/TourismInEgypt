using Tourism.Core.Entities;

namespace Tourism.Core.Repositories.Contract
{
    public interface IPlace_TripRepository : IGenericRepository<Place_Trip>
    {
        Task<Place_Trip> GetOneWithPlaceAndTrip(int id);
        Task<IEnumerable<Place_Trip>> GetAllWithPlaceAndTrip();

        Task<Place_Trip> ForUnique(int valueofplace, int trip);

        Task<IEnumerable<Place_Trip>> GetTripWithplaces(int id);
    }
}

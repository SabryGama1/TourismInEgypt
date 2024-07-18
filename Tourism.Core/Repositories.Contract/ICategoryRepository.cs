using Tourism.Core.Entities;

namespace Tourism.Core.Repositories.Contract
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        //Task<List<Place>> GetAllPlacesBySpecificCategory(int id);
    }
}

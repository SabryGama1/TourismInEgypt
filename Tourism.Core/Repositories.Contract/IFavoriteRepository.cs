using Tourism.Core.Entities;
using Tourism.Core.Helper.DTO;

namespace Tourism.Core.Repositories.Contract
{
    public interface IFavoriteRepository
    {
        Task AddFavorite(Favorite userFav);

        Task DeleteFavorite(FavoriteDTO userFav);
        Task DeletePlaceFromFavorite(int id);
        Task<IEnumerable<ReturnFavoritesDTO>> GetAllFavoriteByUserIdAsync(int UserId);
    }
}

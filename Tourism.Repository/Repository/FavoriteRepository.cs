using Microsoft.EntityFrameworkCore;
using Tourism.Core.Entities;
using Tourism.Core.Helper.DTO;
using Tourism.Core.Repositories.Contract;
using Tourism.Repository.Data;

namespace Tourism.Repository.Repository
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly TourismContext _context;

        public FavoriteRepository(TourismContext context)
        {
            _context = context;
        }


        public async Task AddFavorite(Favorite userFav)
        {
            var findByUserIdAndPlaceId = await _context.Favorites.FirstOrDefaultAsync(r => r.UserId == userFav.UserId && r.PlaceId == userFav.PlaceId);

            if (findByUserIdAndPlaceId != null)
            {
                throw new InvalidOperationException("This Place already Added To Favorite .");
            }
            else
            {
                Place place = await _context.Places.FindAsync(userFav.PlaceId);
                if (place == null)
                {
                    throw new ArgumentException($"This Place Doesn't Exist");
                }
                ApplicationUser finduser = await _context.Users.FindAsync(userFav.UserId);

                if (finduser == null)
                {
                    throw new ArgumentException($"This User Doesn't Exist");
                }

                _context.Favorites.Add(userFav);
                await _context.SaveChangesAsync();
            }

            //return "Added";
        }

        public async Task DeleteFavorite(FavoriteDTO favoriteDTO)
        {
            var favorite = await _context.Favorites
                                         .FirstOrDefaultAsync(f => f.PlaceId == favoriteDTO.PlaceId && f.UserId == favoriteDTO.UserId);
            if (favorite != null)
            {
                _context.Favorites.Remove(favorite);
                await _context.SaveChangesAsync();
            }
        }


        public async Task DeletePlaceFromFavorite(int id)
        {
            var UserFavs = await _context.Favorites.FindAsync(id);
            if (UserFavs != null)
            {
                _context.Favorites.Remove(UserFavs);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("This Favorite Not Exist");
            }
        }



        public async Task<IEnumerable<ReturnFavoritesDTO>> GetAllFavoriteByUserIdAsync(int UserId)
        {
            return await _context.Favorites
                .Include(f => f.Place)
                .Include(f => f.User)
                .Where(f => f.UserId == UserId)
                .Select(f => new ReturnFavoritesDTO
                {
                    Id = f.Id,
                    Name = f.Place.Name,
                    Description = f.Place.Description,
                    Location = f.Place.Location,
                    IsFav = f.IsActive,
                    Rating = f.Place.Rating,
                    Link = f.Place.Link,
                    city = f.Place.City.Name,
                    photos = f.Place.Photos.Select(p => new PhotoDTO
                    {
                        Id = p.Id,
                        Photo = "http://tourisminegypt.somee.com/" + p.Photo
                    }).ToList()
                }).ToListAsync(); ;
        }
    }
}

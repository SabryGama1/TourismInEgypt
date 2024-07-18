using AutoMapper;
using Tourism.Core.Entities;
using Tourism.Core.Helper.DTO;

namespace Tourism.Core.Helper
{

    public class MapperConfig : Profile
    {



        public MapperConfig()
        {
            CreateMap<Place, PlaceDTO>().ForMember(b => b.Category, o => o.MapFrom(b => b.Category.Name))
                .ForMember(t => t.City, o => o.MapFrom(t => t.City.Name))
               .ForMember(p => p.Photos, o => o.MapFrom<PhotoPlaceResolved>());

            CreateMap<Place, PlaceSpecificDTO>()
               .ForMember(t => t.City, o => o.MapFrom(t => t.City.Name))
              .ForMember(p => p.Photos, o => o.MapFrom<PhotoPlaceResolvedSpecific>());

            CreateMap<Category, CategorySpecificWithPlaceDTO>().ForMember(i => i.ImgUrl, i => i.MapFrom<CategoryPhotoResolved>()).ReverseMap();

            // CreateMap<Category, CategoryDTO>().ForMember(i =>i.ImgUrl,i=>i.MapFrom<CategoryPhotoResolved>()).ReverseMap();

            CreateMap<Category, CategoryDetailsDTO>().ForMember(i => i.ImgUrl, i => i.MapFrom<CategoryPhotoResolvedDetials>()).ReverseMap();

            CreateMap<City, CityDTO>()
              .ForMember(c => c.cityPhotos, o => o.MapFrom<PhotoCityResolved>());

            CreateMap<Review, ReviewDTO>().ReverseMap();

            CreateMap<Review, AddReviewDTO>().ReverseMap();
            CreateMap<Favorite, FavoriteDTO>().ReverseMap();
            CreateMap<Favorite, ReturnFavoritesDTO>().ReverseMap();
            CreateMap<ContactDTO, ContactUs>().ReverseMap();


            CreateMap<Trip, TripDTO>()
                .ForMember(d => d.ImgUrl, o => o.MapFrom<PhotoTripDetailsResolved>())
                .ReverseMap();



            CreateMap<Trip, SimpleTripDto>()
                .ForMember(i => i.Photo, i => i.MapFrom<PhotoTripResolved>()).ReverseMap();


            CreateMap<Place, placeOfTripDto>()
                .ForMember(t => t.placeName, o => o.MapFrom(t => t.Name)).ReverseMap();

        }

    }
}

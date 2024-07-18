using AutoMapper;
using Tourism.Core.Entities;
using TourismMVC.ViewModels;

namespace TourismMVC.Helpers
{
    public class MappingProfiles : Profile
    {


        public MappingProfiles()
        {
            CreateMap<PlaceViewModel, Place>().ReverseMap();
            CreateMap<City, CityViewModel>().ReverseMap();

            CreateMap<Category, CategoryViewModel>().ReverseMap();

            CreateMap<CityPhotosViewModel, CityPhotos>().ReverseMap();

            CreateMap<PlacePhotoViewModel, PlacePhotos>().ReverseMap();
            CreateMap<Place_TripModel, Place_Trip>().ReverseMap();
            CreateMap<RoleViewModel, ApplicationRole>()
                .ForMember(AR => AR.Name, RV => RV.MapFrom(v => v.RoleName)).ReverseMap();


            CreateMap<Trip, TripViewModel>().ReverseMap();
        }
    }
}

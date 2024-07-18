using AutoMapper;
using Microsoft.Extensions.Configuration;
using Tourism.Core.Entities;
using Tourism.Core.Helper.DTO;

namespace Tourism.Core.Helper
{
    public class PhotoCityResolved : IValueResolver<City, CityDTO, IEnumerable<PhotoDTO>>
    {
        private readonly IConfiguration configuration;

        public PhotoCityResolved(IConfiguration configuration)
        {
            this.configuration = configuration;
        }


        public IEnumerable<PhotoDTO> Resolve(City source, CityDTO destination, IEnumerable<PhotoDTO> destMember, ResolutionContext context)
        {
            List<PhotoDTO> dtoList = new List<PhotoDTO>();

            foreach (var cityPhoto in source.CityPhotos)
            {
                PhotoDTO dto = new PhotoDTO();
                dto.Id = cityPhoto.Id;
                dto.Photo = $"{configuration["ApiBaseUrl"]}/{cityPhoto.Photo}";

                dtoList.Add(dto);
            }

            return dtoList;
        }
    }

}

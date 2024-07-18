using AutoMapper;
using Microsoft.Extensions.Configuration;
using Tourism.Core.Entities;
using Tourism.Core.Helper.DTO;

namespace Tourism.Core.Helper
{
    public class PhotoPlaceResolved : IValueResolver<Place, PlaceDTO, IEnumerable<PhotoDTO>>
    {
        private readonly IConfiguration configuration;

        public PhotoPlaceResolved(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IEnumerable<PhotoDTO> Resolve(Place source, PlaceDTO destination, IEnumerable<PhotoDTO> destMember, ResolutionContext context)
        {
            List<PhotoDTO> dtoList = new List<PhotoDTO>();

            foreach (var placePhoto in source.Photos)
            {
                PhotoDTO dto = new PhotoDTO();
                dto.Id = placePhoto.Id;
                dto.Photo = $"{configuration["ApiBaseUrl"]}/{placePhoto.Photo}";

                dtoList.Add(dto);
            }

            return dtoList;
        }
    }

}

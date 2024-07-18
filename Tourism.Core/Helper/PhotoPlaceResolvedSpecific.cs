using AutoMapper;
using Microsoft.Extensions.Configuration;
using Tourism.Core.Entities;
using Tourism.Core.Helper.DTO;

namespace Tourism.Core.Helper
{
    public class PhotoPlaceResolvedSpecific : IValueResolver<Place, PlaceSpecificDTO, IEnumerable<PhotoDTO>>
    {
        private readonly IConfiguration configuration;

        public PhotoPlaceResolvedSpecific(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IEnumerable<PhotoDTO> Resolve(Place source, PlaceSpecificDTO destination, IEnumerable<PhotoDTO> destMember, ResolutionContext context)
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

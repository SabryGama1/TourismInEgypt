using AutoMapper;
using Microsoft.Extensions.Configuration;
using Tourism.Core.Entities;
using Tourism.Core.Helper.DTO;

namespace Tourism.Core.Helper
{
    public class PhotoTripResolved : IValueResolver<Trip, SimpleTripDto, string>
    {
        private readonly IConfiguration configuration;

        public PhotoTripResolved(IConfiguration configuration)
        {
            this.configuration = configuration;
        }


        public string Resolve(Trip source, SimpleTripDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ImgUrl))
                return $"{configuration["ApiBaseUrl"]}/{source.ImgUrl}";

            return string.Empty;
        }
    }
}

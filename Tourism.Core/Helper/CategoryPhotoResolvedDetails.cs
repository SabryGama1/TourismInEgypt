using AutoMapper;
using Microsoft.Extensions.Configuration;
using Tourism.Core.Entities;
using Tourism.Core.Helper.DTO;

namespace Tourism.Core.Helper
{
    public class CategoryPhotoResolvedDetials : IValueResolver<Category, CategoryDetailsDTO, string>
    {
        private readonly IConfiguration configuration;

        public CategoryPhotoResolvedDetials(IConfiguration configuration)
        {
            this.configuration = configuration;
        }


        public string Resolve(Category source, CategoryDetailsDTO destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ImgUrl))
                return $"{configuration["ApiBaseUrl"]}/{source.ImgUrl}";

            return string.Empty;
        }
    }
}

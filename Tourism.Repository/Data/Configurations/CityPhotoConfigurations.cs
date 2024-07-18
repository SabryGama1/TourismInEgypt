using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tourism.Core.Entities;

namespace Tourism.Repository.Data.Configurations
{
    public class CityPhotoConfigurations : IEntityTypeConfiguration<CityPhotos>
    {
        public void Configure(EntityTypeBuilder<CityPhotos> builder)
        {
            builder.HasOne<City>(CP => CP.city)
                .WithMany(c => c.CityPhotos)
                .HasForeignKey(CP => CP.CityId);

            builder.HasKey(CP => CP.Id);
        }
    }
}

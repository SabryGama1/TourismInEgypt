using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tourism.Core.Entities;

namespace Tourism.Repository.Data.Configurations
{
    public class PlacePhotosConfigurations : IEntityTypeConfiguration<PlacePhotos>
    {

        public void Configure(EntityTypeBuilder<PlacePhotos> builder)
        {


            builder.HasOne<Place>(PP => PP.Place)
                  .WithMany(p => p.Photos)
                  .HasForeignKey(PP => PP.PlaceId);

            builder.HasKey(PP => PP.Id);
        }
    }
}

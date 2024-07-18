using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tourism.Core.Entities;

namespace Tourism.Repository.Data.Configurations
{
    public class PlaceConfigurtions : IEntityTypeConfiguration<Place>
    {
        public void Configure(EntityTypeBuilder<Place> builder)
        {
            builder.Property(P => P.Name).IsRequired().HasMaxLength(100);
            builder.Property(P => P.Description).IsRequired();
            builder.Property(P => P.Location).IsRequired();
            builder.Property(P => P.Rating).IsRequired();
            builder.Property(P => P.Phone).IsRequired();
            builder.Property(P => P.Link).IsRequired();

            builder.HasOne<Category>(P => P.Category)
                   .WithMany(c => c.Places)
                   .HasForeignKey(P => P.CategoryId);

            builder.HasOne<City>(P => P.City)
                   .WithMany(c => c.Places)
                   .HasForeignKey(P => P.CityId);

        }
    }
}

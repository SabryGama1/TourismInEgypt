using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tourism.Core.Entities;

namespace Tourism.Repository.Data.Configurations
{
    public class CityConfigurations : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.Property(C => C.Name).HasMaxLength(50).IsRequired();
            builder.Property(C => C.Location).IsRequired();
            builder.Property(C => C.Description).IsRequired();

            //builder.HasMany(P => P.Places)
            //     .WithOne(c => c.City)
            //     .HasForeignKey(P => P.CityId);
        }
    }
}

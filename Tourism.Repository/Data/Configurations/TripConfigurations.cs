using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tourism.Core.Entities;

namespace Tourism.Repository.Data.Configurations
{
    public class TripConfigurations : IEntityTypeConfiguration<Trip>
    {
        public void Configure(EntityTypeBuilder<Trip> builder)
        {
            builder.Property(C => C.City).HasMaxLength(50).IsRequired();
            builder.Property(x => x.StartDate).IsRequired();
            builder.Property(x => x.EndDate).IsRequired();

            builder.Property(x => x.Description).IsRequired();

            builder.HasMany(x => x.Users)
                .WithMany(x => x.Trips)
                .UsingEntity<User_Trip>();

            builder.HasMany(x => x.Places)
                .WithMany(x => x.Trips)
                .UsingEntity<Place_Trip>();
            builder.ToTable("Trips");
        }
    }
}

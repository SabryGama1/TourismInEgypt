using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tourism.Core.Entities;

namespace Tourism.Repository.Data.Configurations
{
    public class Place_TripConfigurations : IEntityTypeConfiguration<Place_Trip>
    {
        public void Configure(EntityTypeBuilder<Place_Trip> builder)
        {
            builder.HasKey(x => x.Id);

            builder.ToTable("Place_Trips");
        }
    }
}

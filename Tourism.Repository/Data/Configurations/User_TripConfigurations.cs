using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tourism.Core.Entities;

namespace Tourism.Repository.Data.Configurations
{
    public class User_TripConfigurations : IEntityTypeConfiguration<User_Trip>
    {
        public void Configure(EntityTypeBuilder<User_Trip> builder)
        {
            builder.HasKey(x => x.Id);

            builder.ToTable("User_Trips");
        }

    }
}

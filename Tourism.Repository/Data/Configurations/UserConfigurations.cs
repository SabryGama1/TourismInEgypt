using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tourism.Core.Entities;

namespace Tourism.Repository.Data.Configurations
{
    public class UserConfigurations : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(x => x.FName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.LName).HasMaxLength(50);
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.UserName).IsRequired();
            builder.HasKey(x => x.Id);
            //builder.HasMany(x => x.Places)
            //    .WithMany(x => x.Users).UsingEntity<Favorite>();

            builder.ToTable("Users");

        }
    }
}

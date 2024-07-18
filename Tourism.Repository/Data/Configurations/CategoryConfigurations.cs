using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tourism.Core.Entities;

namespace Tourism.Repository.Data.Configurations
{
    public class CategoryConfigurations : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(C => C.Name).HasMaxLength(100).IsRequired();
            //builder.HasMany(P => P.Places)
            //      .WithOne(c => c.Category)
            //      .HasForeignKey(P => P.CategoryId);
        }
    }
}

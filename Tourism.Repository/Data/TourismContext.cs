using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Tourism.Core.Entities;

namespace Tourism.Repository.Data
{
    public class TourismContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {

        public TourismContext()
        {
        }


        public TourismContext(DbContextOptions<TourismContext> options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=TourismInEgypt.mssql.somee.com;Database=TourismInEgypt;user id=Sabry_SQLLogin_1;pwd=vf4uin2fa8;MultipleActiveResultSets=true;TrustServerCertificate=True");


        }

        public DbSet<Trip> Trips { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<CityPhotos> CityPhotos { get; set; }
        public DbSet<PlacePhotos> PlacePhotos { get; set; }
        public DbSet<User_Trip> User_Trips { get; set; }
        public DbSet<Place_Trip> Place_Trips { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Favorite> Favorites { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<ResetPassword> Passwords { get; set; }

        public DbSet<ContactUs> ContactUs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserLogin<int>>().HasKey(t => new { t.ProviderKey, t.LoginProvider });
            modelBuilder.Entity<IdentityUserRole<int>>().HasKey(t => new { t.RoleId, t.UserId });
            modelBuilder.Entity<IdentityUserToken<int>>().HasKey(t => new { t.UserId, t.LoginProvider });
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
    }
}

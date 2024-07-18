using Microsoft.EntityFrameworkCore;
using Tourism.Core.Entities;
using Tourism.Core.Repositories.Contract;
using Tourism.Repository;
using Tourism.Repository.Data;
using Tourism.Service;
using TourismMVC.Helpers;

namespace TourismMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<TourismContext>(
               options =>
               {
                   options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("conn"));
                   // options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

               });

            builder.Services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped(typeof(IAuthService), typeof(AuthService));

            builder.Services.AddAutoMapper(m => m.AddProfile(new MappingProfiles()));

            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(
            config =>
            {
                //config.Password.RequireUppercase = false;
                //config.Lockout.MaxFailedAccessAttempts = 3;
                //config.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);


            })//configuration
                .AddEntityFrameworkStores<TourismContext>();



            builder.Services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = "/Account/Login";
                //config.ExpireTimeSpan= TimeSpan.FromMinutes(5);
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
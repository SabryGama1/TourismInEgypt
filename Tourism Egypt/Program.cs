using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Tourism.Core.Entities;
using Tourism.Core.Helper;
using Tourism.Core.Repositories.Contract;
using Tourism.Repository;
using Tourism.Repository.Data;
using Tourism.Repository.Repository;
using Tourism.Service;

namespace Tourism_Egypt
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();

            //dbcontext
            #region Container Services
            builder.Services.AddDbContext<TourismContext>(
                   options => options.UseSqlServer(builder.Configuration.GetConnectionString("conn")));
            builder.Services.AddScoped(typeof(ICityRepository), typeof(CityRepository));
            builder.Services.AddScoped(typeof(IPlaceRepository), typeof(PlaceRepository));
            builder.Services.AddScoped(typeof(ICategoryRepository), typeof(CategoryRepository));
            builder.Services.AddScoped(typeof(INotificationRepository), typeof(NotificationRepository));
            builder.Services.AddScoped(typeof(IFavoriteRepository), typeof(FavoriteRepository));
            builder.Services.AddScoped(typeof(IReviewRepository), typeof(ReviewRepository));
            builder.Services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            builder.Services.AddAutoMapper(typeof(MapperConfig));
            builder.Services.AddScoped(typeof(ITripRepository), typeof(TripRepository));

            var configuration = builder.Configuration;

            // Add services to the container.
            builder.Services.Configure<EmailConfiguration>(configuration.GetSection("EmailConfiguration"));
            builder.Services.AddScoped<IEmailService, EmailService>();
            //builder.Services.AddAuthentication(o =>
            //{
            //    o.DefaultAuthenticateScheme = GoogleDefaults.AuthenticationScheme;
            //    o.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;

            //})
            //    .AddGoogle(options=>
            //    {
            //        IConfigurationSection GoogleAuthSec = builder.Configuration.GetSection("Authentication:Google");
            //       options.ClientId=GoogleAuthSec["ClientId"];
            //        options.ClientSecret = GoogleAuthSec["ClientSecret"];


            //    });
            #endregion

            #region Identity Services
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped(typeof(IAuthService), typeof(AuthService));
            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(
            config =>
            {
                config.Password.RequireUppercase = false;
                config.Lockout.MaxFailedAccessAttempts = 3;
                config.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);


            })//configuration
                .AddEntityFrameworkStores<TourismContext>()
                .AddDefaultTokenProviders();

            //Authentication Schema
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).
                AddJwtBearer(options =>
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:ValidAudience"],
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:secretKey"])),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromDays(double.Parse(builder.Configuration["JWT:Duration"]))
                }
                ).AddGoogle(options =>
                {
                    IConfigurationSection GoogleAuthSec = builder.Configuration.GetSection("Authentication:Google");
                    options.ClientId = GoogleAuthSec["ClientId"];
                    options.ClientSecret = GoogleAuthSec["ClientSecret"];
                    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
                    options.ClientId = builder.Configuration["Authentication:Google:ClientId"]; ;

                    options.CallbackPath = "/auth/google-callback";

                });
            #endregion




            builder.Services.Configure<DataProtectionTokenProviderOptions>(
                opt => opt.TokenLifespan = TimeSpan.FromHours(10));



            builder.Services.AddControllers();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Tourism", Version = "v1" });
            });

            builder.Services.AddSwaggerGen(swagger =>
            {
                //This is to generate the Default UI of Swagger Documentation    
                swagger.SwaggerDoc("v2", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Tourism In Egypt",
                    Description = "Different types of tourism in Egypt"
                });

                // To Enable authorization using Swagger (JWT)    
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
                });

                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] { }
                        }

        });
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(corsOptions =>
            {
                corsOptions.AddPolicy("MyPolicy", corsPolicyBuilder =>
                {
                    corsPolicyBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });

            var app = builder.Build();




            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            app.UseSwagger();
            app.UseSwaggerUI();
            //}

            app.UseCors("MyPolicy");

            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/")
                {
                    context.Response.Redirect("/swagger/index.html");
                    return;
                }
                await next();

            });



            //var app = builder.Build();


            #region Update DB

            //Explicitly
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;

            var loggerfactury = services.GetRequiredService<ILoggerFactory>();
            var dbContext = services.GetRequiredService<TourismContext>();
            try
            {

                await dbContext.Database.MigrateAsync();

            }
            catch (Exception ex)
            {
                var logger = loggerfactury.CreateLogger<Program>();//return to main 
                logger.LogError(ex, "Erro Occured during apply migration");

            }

            #endregion


            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            app.UseSwagger();
            app.UseSwaggerUI();
            //}

            app.UseCors("MyPolicy");

            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/")
                {
                    context.Response.Redirect("/swagger/index.html");
                    return;
                }
                await next();
            });

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();

        }
    }
}



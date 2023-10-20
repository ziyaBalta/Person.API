using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Person.Data.Auth;
using Person.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Person.API
{
    public static class Extensions
    {
        private static string _connStr = "";
        public static void AddDatabaseContext(this IServiceCollection services, string connectionString, string runMode, PrjSettings prjSettings)
        {
            _connStr = connectionString;
            if (runMode == "dev")
                ConfigureDevelopmentServices(services);
            else if (runMode == "test")
                ConfigureTestingServices(services);
            else if (runMode == "prod")
                ConfigureProductionServices(services);

            services.AddDbContext<AppDbContext>(options =>
                    options.UseNpgsql(connectionString, o => o.SetPostgresVersion(new Version(11, 13))));

            services.AddIdentity<ApplicationUser, IdentityRole>()
               .AddEntityFrameworkStores<AppDbContext>()
               .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.User.RequireUniqueEmail = true;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
            });

            // Adding Authentication  
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            // Adding Jwt Bearer  
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = prjSettings.ValidAudience,
                    ValidIssuer = prjSettings.ValidIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(prjSettings.JwtSecret))
                };
            });
        }

        public static void ConfigureDevelopmentServices(IServiceCollection services)
        {
            // use in-memory database
            //ConfigureInMemoryDatabases(services);

            // use real database
            ConfigureProductionServices(services);
        }

        public static void ConfigureProductionServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(c =>
                c.UseNpgsql(_connStr, o => o.SetPostgresVersion(new Version(11, 13))));

            //services.AddDbContext<ApplicationDbContext>(c =>
            //    c.UseNpgsql(_connStr, o => o.SetPostgresVersion(new Version(11, 13))));

        }

        private static void ConfigureInMemoryDatabases(IServiceCollection services)
        {
            //services.AddDbContext<AppDbContext>(c =>
            //    c.UseInMemoryDatabase("Payment"));

            //services.AddDbContext<ApplicationDbContext>(c =>
            //    c.UseInMemoryDatabase("Identity"));
        }

        public static void ConfigureTestingServices(IServiceCollection services)
        {
            ConfigureInMemoryDatabases(services);
        }

    }
}

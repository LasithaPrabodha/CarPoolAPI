using CarPool.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using CarPool.Application.Contracts;
using CarPool.Infrastructure.Persistence;
using CarPool.Infrastructure.Repositories;
using CarPool.Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CarPool.Application.Models;
using Mapster;
using System.Reflection;
using MapsterMapper;

namespace CarPool.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        services.AddDatabasePersistence(configuration);
        services.AddRepositories();
        services.AddIdentity();
        services.AddAuthentication(configuration);
        
        services.Configure<IdentityOptions>(opts => opts.Password.RequireLowercase = true);

        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();
    }

    public static void AddSharedServices(this IServiceCollection services)
    {
        services.AddScoped<IDateTime, DateTimeService>();
    }

    private static void AddDatabasePersistence(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<IdentityDbContext>(options =>
                options.UseInMemoryDatabase("IdentityDb"));
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("ApplicationDb"));

        }
        else
        {
            services.AddDbContext<IdentityDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("IdentityConnection")));
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("ApplicationConnection")));
        }
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<PublishDomainEventsInterceptor>();
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();
        services.AddScoped<ITripRepository, TripRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
    }

    private static void AddIdentity(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
        {
            options.Password.RequireNonAlphanumeric = false;
        })
            .AddRoleManager<RoleManager<ApplicationRole>>()
            .AddEntityFrameworkStores<IdentityDbContext>()
            .AddDefaultTokenProviders();

        var passwordOptions = new PasswordOptions()
        {
            RequireDigit = false, //in accordance with ASVS 4.0
            RequiredLength = 10, //in accordance with ASVS 4.0
            RequireUppercase = false, //in accordance with ASVS 4.0
            RequireLowercase = false //in accordance with ASVS 4.0
        };

        services.AddScoped(a => passwordOptions);

        services.Configure<IdentityOptions>(options =>
        {
            options.Password = passwordOptions;

        });
        services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
        services.AddScoped<IApplicationUserService, ApplicationUserService>();
    }

    private static void AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = configuration["JwtSettings:Issuer"],
                ValidAudience = configuration["JwtSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]))

            };
        });
    }

}
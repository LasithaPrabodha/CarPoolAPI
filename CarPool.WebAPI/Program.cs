using System.Reflection;
using CarPool.Application.Contracts;
using CarPool.Application.Extensions;
using CarPool.Infrastructure.Extensions;
using CarPool.WebAPI.Services;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Retrieve the connection string
string connectionString = builder.Configuration.GetConnectionString("AppConfig");

// Load configuration from Azure App Configuration
builder.Configuration.AddAzureAppConfiguration(connectionString);

builder.Services.AddControllers();
builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructureLayer(builder.Configuration);
builder.Services.AddSharedServices();

var config = TypeAdapterConfig.GlobalSettings;
config.Scan(Assembly.GetExecutingAssembly());

builder.Services.AddSingleton(config);
builder.Services.AddScoped<IMapper, ServiceMapper>();

builder.Services.AddTransient<IAuthenticatedUserService, AuthenticatedUserService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup =>
{
    // Include 'SecurityScheme' to use JWT Authentication
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });

});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


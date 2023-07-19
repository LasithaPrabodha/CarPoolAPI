using AutoMapper.Extensions.ExpressionMapping;
using CarPool.Application.Contracts;
using CarPool.Application.Extensions;
using CarPool.Infrastructure.Extensions;
using CarPool.WebAPI.Mappings;
using CarPool.WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructureLayer(builder.Configuration);
builder.Services.AddSharedServices();

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<AppProfile>();
    config.AddExpressionMapping();
});

builder.Services.AddTransient<IAuthenticatedUserService, AuthenticatedUserService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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


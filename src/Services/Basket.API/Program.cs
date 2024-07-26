using Basket.API.Data;
using Basket.API.Models;
using Carter;
using Marten; 
using Microsoft.Extensions.Caching.Distributed;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCarter();

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddMarten(option =>
{
    option.Connection(builder.Configuration.GetConnectionString("Database")!);
    option.Schema.For<ShopingCart>().Identity(x => x.UserName);
}).UseLightweightSessions();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();
builder.Services.AddStackExchangeRedisCache(option =>
{
    option.Configuration = builder.Configuration.GetConnectionString("Redis"); 
});
//builder.Services.AddScoped<IBasketRepository>(pro =>
//{
//    var basketRepository = pro.GetRequiredService<BasketRepository>();
//    return new CachedBasketRepository(basketRepository, pro.GetRequiredService<IDistributedCache>());
//});
builder.Services.AddScoped<IBasketRepository, CachedBasketRepository>();
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
var app = builder.Build();

app.MapCarter();
app.UseExceptionHandler(o => { });
app.Run();

using Basket.API.Models;
using Carter;
using Marten;

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
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapCarter();
app.Run();

using BuildingBlocks.Behaviors;
using Carter;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);   
builder.Services.AddMarten(option =>
{
    option.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();
var app = builder.Build();

//app.MapGet("/", () => "Hello World!");
app.MapCarter();
app.Run();

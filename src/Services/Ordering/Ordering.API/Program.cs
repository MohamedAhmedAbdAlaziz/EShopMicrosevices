using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
builder.Services.AddApplicationServices().AddInfrastructureServices(builder.Configuration).AddAPIServices();
app.MapGet("/", () => "Hello World!");

app.Run();

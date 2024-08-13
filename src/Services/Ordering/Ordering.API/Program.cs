using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Data.Extentions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplicationServices().AddInfrastructureServices(builder.Configuration).AddAPIServices();
var app = builder.Build();
app.UseAPIServices();
app.MapGet("/", () => "Hello World!");
if (app.Environment.IsDevelopment())
{
    await app.InitaliseDatabaseAsync();
}

app.Run();

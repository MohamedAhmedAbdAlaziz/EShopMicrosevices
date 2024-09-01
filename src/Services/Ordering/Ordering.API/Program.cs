using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Data.Extentions;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices(builder.Configuration);


var app = builder.Build();
app.UseApiServices();
app.MapGet("/", () => "Hello World!");


if (app.Environment.IsDevelopment())
{
    await app.InitaliseDatabaseAsync();
}

app.Run();



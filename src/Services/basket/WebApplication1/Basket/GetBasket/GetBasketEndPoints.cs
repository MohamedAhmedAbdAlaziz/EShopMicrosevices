using Basket.API.Models;
using Carter;

namespace Basket.API.Basket.GetBasket
{
    public class GetBasketResponse(ShopingCart cart);
    public class GetBasketEndPoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/basket/{userName}", async (string userName, ISender sender) =>
            {
                var result = await sender.Send(new GetBasketQuery(userName));
                var response = result.Adapt<GetBasketResponse>();

                return Results.Ok(response);
            })
              .WithName("Getbasket byId")
              .Produces<GetBasketResponse>(StatusCodes.Status200OK)
              .ProducesProblem(StatusCodes.Status400BadRequest)
              .WithSummary("Get Product By Id")
              .WithDescription("Get basket By Id");

        }
    }
}

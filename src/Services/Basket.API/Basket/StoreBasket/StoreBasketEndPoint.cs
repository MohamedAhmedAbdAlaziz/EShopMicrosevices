using Basket.API.Models;
using Carter;

namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketRequest(ShopingCart Cart);
    public record StoreBasketReponse(string UserName);

    public class StoreBasketEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket", async (StoreBasketRequest request, ISender sender) =>
            {
                var command = request.Adapt<StoreBasketCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<StoreBasketReponse>();
                return Results.Created($"/basket/{response.UserName}",response);

            }).WithName("Store Basket")
                .Produces<StoreBasketReponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Store Basket")
                .WithDescription("Store Basket");
        }
    }
}

using Basket.API.Dtos;

namespace Basket.API.Basket.ChechoutBasket
{
    public record CheckoutRequest(BasketCheckoutDto BasketCheckoutDto);
  
    public record CheckoutBasketResponse(bool IsSuccess);
    public class CheckoutBasketEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket/checkout", async (CheckoutRequest request, ISender sender) =>
            {
                var command = request.Adapt<CheckoutCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<CheckoutBasketResponse>();

                return result;

            }
            )
                .WithName("CheckoutBasket")
                .Produces<CheckoutBasketResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Checkout Basket")
                .WithDescription("Checkout Basket");

        }
    }
}

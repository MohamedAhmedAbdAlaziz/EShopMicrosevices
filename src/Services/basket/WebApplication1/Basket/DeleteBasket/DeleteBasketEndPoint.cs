using Carter;

namespace Basket.API.Basket.DeleteBasket
{
    public record DeleteBasketRequest(Guid Id);
    public record DeleteBasketResponse(bool IsSuccess);

    public class DeleteBasketEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/Baskets/{userName}",
                async (string userName, ISender sender) =>
                {
                    //  var command = request.Adapt<DeleteBasketCommand>();
                    var result = await sender.Send(new DeleteBasketCommand(userName));

                    var response = result.Adapt<DeleteBasketResponse>();

                    return Results.Ok(response);
                }
                ).WithName("DeleteBasket")
                .Produces<DeleteBasketResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Delete Basket")
                .WithDescription("Delete Basket");

        }
    }
}

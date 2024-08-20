using Ordering.Application.Orders.Commands.UpdateOrder;
using Ordering.Application.Orders.Queries.GetOrdersByName;

namespace Ordering.API.EndPoints
{
    public record GetOrdersbyNameResponse(IEnumerable<OrderDto> Orders);
    public class GetOrdersbyName : ICarterModule
    {

        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders/{orderName}", async (string orderName, ISender sender) =>
            {

                var result = await sender.Send(new GetOrdersByNameQuery(orderName));
                var response = result.Adapt<GetOrdersbyNameResponse>();
                return Results.Ok(response);

            })
                .WithName("GetOrderbyName")
                .Produces<GetOrdersbyNameResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Order by Name")
                .WithDescription("Get Order by Name");

        }
    }
}

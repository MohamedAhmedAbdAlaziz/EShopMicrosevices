using Ordering.Application.Orders.Commands.UpdateOrder;
using Ordering.Application.Orders.Queries.GetOrdersByCustomer;
using Ordering.Application.Orders.Queries.GetOrdersByName;
using Ordering.Domain.ValueObjects;

namespace Ordering.API.EndPoints
{

    public record GetOrdersbyCustomerResponse(IEnumerable<OrderDto> Orders);
    public class GetOrdersbyCustomer : ICarterModule
    {

        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders/customer/{customerId}", async (Guid customerId, ISender sender) =>
            {

                var result = await sender.Send(new GetOrdersByCustomerQuery(customerId));
                var response = result.Adapt<GetOrdersbyCustomerResponse>();
                return Results.Ok(response);

            })
                .WithName("GetOrdersbyCustomer")
                .Produces<GetOrdersbyCustomerResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Orders by Customer")
                .WithDescription("Get Orders by Customer");

        }
    }
}

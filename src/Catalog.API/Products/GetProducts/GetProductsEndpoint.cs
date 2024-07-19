
using Catalog.API.Products.CreateProduct;

namespace Catalog.API.Products.GetProducts
{

    public record GetProductResponse(IEnumerable<Product> Products);
    public record GetProductsRequest(int? PageNumber , int? PageSize=5);

    public class GetProductsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", async ([AsParameters] GetProductsRequest request, ISender sender) =>
            {
                var query =   request.Adapt<GetProductsQuery>();
                var result = await sender.Send(query);
                var response = result.Adapt<GetProductResponse>();
              return Results.Ok(response);
            })
                .WithName("GetProducts")
                .Produces<GetProductResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Cet Products")
                .WithDescription("Get Product");

        }
    }
}

using Carter;
using Catalog.API.Products.DeleteProduct;
using Mapster;
using MediatR;

namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductRequest(Guid Id );
    public record DeleteProductResponse(bool IsSuccess);
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is Requesrd");
           
        }

    }
    public class DeleteProductEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/products/{id}",
                async (Guid Id, ISender sender) =>
                {
                  //  var command = request.Adapt<DeleteProductCommand>();
                    var result = await sender.Send(new DeleteProductCommand(Id));

                    var response = result.Adapt<DeleteProductResponse>();

                    return Results.Ok(response);
                }
                ).WithName("DeleteProduct")
                .Produces<DeleteProductResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Delete Product")
                .WithDescription("Delete Product");

        }
    }
}

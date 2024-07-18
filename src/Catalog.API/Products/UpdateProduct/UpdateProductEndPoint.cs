using Carter;
using Catalog.API.Products.CreateProduct;
using Mapster;
using MediatR;

namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductRequest(string Id,string Name,
        List<string> Category, string Description, string ImageFile,
        decimal Price);
    public record UpdateProductResponse(bool IsSuccess);
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is Requesrd");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is Requesrd")
                .Length(2, 150).WithMessage("name must be between 2 and 150 digits");
            //RuleFor(x => x.Category).NotEmpty().WithMessage("Category is Requesrd");
            //RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is Requesrd");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be more rhan 0");

        }

    }
    public class DeleteProductEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/products",
                async (UpdateProductRequest request, ISender sender) =>
                {
                    var command = request.Adapt<UpdateProductCommand>();
                    var result = await sender.Send(command);

                    var response = result.Adapt<UpdateProductResponse>();

                    return Results.Ok(response);
                }
                ).WithName("UpdateProduct")
                .Produces<UpdateProductResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Update Product")
                .WithDescription("Update Product");

        }
    }
}

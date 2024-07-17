using BuildingBlocks.CQRS;
using MediatR;
using Catalog.API.Models;
using Marten;
namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name,
      List<string> Category, string Description, string ImageFile,
      decimal Price) : ICommand<CreateProductResult>;

    public record CreateProductResult(Guid Id);
    public class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command,
            CancellationToken cancellationToken)
        {
            var Product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price
            };
            session.Store(Product);
            await session.SaveChangesAsync(cancellationToken);
            return new CreateProductResult(Product.Id);
        }
    }
}

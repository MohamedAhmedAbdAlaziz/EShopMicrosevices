using BuildingBlocks.CQRS;
using MediatR;
using Catalog.API.Models;
using Marten;
using Microsoft.Extensions.Logging;
namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;

    public record DeleteProductResult(bool IsSuccess);
    public class DeleteProductCommandHandler(IDocumentSession session, ILogger<DeleteProductCommandHandler> logger) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand command,
            CancellationToken cancellationToken)
        {
            logger.LogInformation("DeleteProductByCatalogQueryHandler.Handler called with {@query}", command);


            session.Delete<Product>(command.Id);
            await session.SaveChangesAsync(cancellationToken);
            return new DeleteProductResult(true);
        }
    }
}

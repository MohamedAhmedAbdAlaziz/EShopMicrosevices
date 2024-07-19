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

    public class CreateProductCommandValidator:AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is Requesrd");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category is Requesrd");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is Requesrd");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price is Requesrd");

        }
     
    }
    public class CreateProductCommandHandler(IDocumentSession session
       // ,IValidator<CreateProductCommand> validator
      // ,ILogger<CreateProductCommandHandler> Logger
        ) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command,
            CancellationToken cancellationToken)
        {
            //var result = await validator.ValidateAsync(command, cancellationToken);
            //    var errors= result.Errors.Select(x => x.ErrorMessage).ToList();
            //if (errors.Any())
            //{
            //    throw new ValidationException(errors.FirstOrDefault());
            //}
          //  Logger.LogInformation("CreateProductCommandHandler.Hanlder called with {@command}", command);
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

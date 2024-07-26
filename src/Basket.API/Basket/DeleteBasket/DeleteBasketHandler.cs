using Basket.API.Basket.DeleteBasket;
using Basket.API.Basket.StoreBasket;
using Basket.API.Data;
using Basket.API.Models;

namespace Basket.API.Basket.DeleteBasket
{
    public record DeleteBasketCommand(string UserName ) : ICommand<DeleteBasketResult>;

    public record DeleteBasketResult(bool IsSuccess);
    public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
    {
        public DeleteBasketCommandValidator()
        {
            RuleFor(x => x.UserName).NotNull().WithMessage("UserName is requird");
        }
    }

    public class DeleteBasketHandler(IBasketRepository repository) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
    {
        public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
        {
            await repository.Delete(command.UserName, cancellationToken);

             return new DeleteBasketResult(true);
        }
    }
}

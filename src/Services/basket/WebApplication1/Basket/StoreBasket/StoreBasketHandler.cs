using Basket.API.Models;

namespace Basket.API.Basket.StoreBasket;

public record StoreBasketCommand(ShopingCart Cart):ICommand<StoreBasketResult>;

public record StoreBasketResult(string UserName);
public class StoreBasketCommandValidator:AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x.Cart).NotNull().WithMessage("Cart can not be null");
        RuleFor(x => x.Cart.UserName).NotNull().WithMessage("UserName is requird");
    }
}

public class StoreBasketHandler : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        ShopingCart cart = command.Cart;
        return new StoreBasketResult("swn");
    }
}

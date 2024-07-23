﻿using Basket.API.Basket.DeleteBasket;
using Basket.API.Models;

namespace Basket.API.Basket.DeleteBasket
{
    public record DeleteBasketCommand(string UserName ) : ICommand<DeleteBasketResult>;

    public record DeleteBasketResult(bool ISuccess);
    public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
    {
        public DeleteBasketCommandValidator()
        {
            RuleFor(x => x.UserName).NotNull().WithMessage("UserName is requird");
        }
    }

    public class DeleteBasketHandler : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
    {
        public async Task<DeleteBasketResult> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
        {
            return new DeleteBasketResult(true);
        }
    }
}

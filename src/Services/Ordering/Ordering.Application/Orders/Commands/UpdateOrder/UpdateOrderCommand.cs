using BuildingBlocks.CQRS;
using FluentValidation;
using Ordering.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ordering.Application.Orders.Commands.UpdateOrder
{
    public record UpdateOrderCommand(OrderDto Order):ICommand<UpdateOrderResult>;

    public record UpdateOrderResult(bool IsSuccess);

    public class UpdateOrderCommandValidator :AbstractValidator<UpdateOrderCommand>
    { 
      public UpdateOrderCommandValidator()
        {
            RuleFor(x => x.Order.Id).NotEmpty().WithMessage("Id is reqired");
            RuleFor(x => x.Order.OrderName).NotEmpty().WithMessage("Name is reqired");
            RuleFor(x => x.Order.CustomerId).NotNull().WithMessage("CustomerId is reqired");
        }
    }

}

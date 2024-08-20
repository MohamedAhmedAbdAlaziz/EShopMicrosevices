using BuildingBlocks.CQRS;
using Ordering.Application.Data;
using Ordering.Application.Dtos;
using Ordering.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Orders.Commands.UpdateOrder
{
    public class UpdateOrderHandler(IApplicationDbContext dbContext) : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
    {
        public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {

            var orderId = OrderId.Of(command.Order.Id);
            var order = await dbContext.Orders.FindAsync([orderId], cancellationToken: cancellationToken);
         if(order is null)
            {
                throw new OrderNotFoundException(command.Order.Id);
            }
            UpdateOrderWithNewValuesValues(order, command.Order);
            dbContext.Orders.Update(order);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new UpdateOrderResult(true);

          //   throw new NotImplementedException();
        }

        private void UpdateOrderWithNewValuesValues(Order order, OrderDto orderDto)
        {
            var updateShippingAddress = Address.Of(orderDto.ShippingAddress.FirstName, orderDto.ShippingAddress.LastName, orderDto.ShippingAddress.EmailAddress , orderDto.ShippingAddress.AddressLine, orderDto.ShippingAddress.Country, orderDto.ShippingAddress.State, orderDto.ShippingAddress.ZipCode);  
            var updateBillinggAddress = Address.Of(orderDto.BillinggAddress.FirstName, orderDto.BillinggAddress.LastName, orderDto.BillinggAddress.EmailAddress , orderDto.BillinggAddress.AddressLine, orderDto.BillinggAddress.Country, orderDto.BillinggAddress.State, orderDto.BillinggAddress.ZipCode);
            var updatePayment = Payment.Of(orderDto.Payment.CardName, orderDto.Payment.CardNumber, orderDto.Payment.Expiration, orderDto.Payment.Cvv, orderDto.Payment.PaymentMethod) ;
            order.Update(
                orderName:OrderName.Of(orderDto.OrderName), 
                shippingAddress: updateBillinggAddress, 
                billingAddress:updateBillinggAddress,
                payment:updatePayment,
                status:orderDto.Status
                );
        }
    }  
}

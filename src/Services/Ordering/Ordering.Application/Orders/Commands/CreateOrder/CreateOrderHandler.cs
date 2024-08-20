using BuildingBlocks.CQRS;
using Ordering.Application.Data;
using Ordering.Application.Dtos;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderHandler(IApplicationDbContext dbContext) : ICommandHandler<CreateOrderCommand, CreateOrderResult>
    {
        public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {

            var order = CreateNewOrder(command.Order);
            dbContext.Orders.Add(order);
            await dbContext.SaveChangesAsync(cancellationToken);
            return new CreateOrderResult(order.Id.Value) ;
         
        }

        private Order CreateNewOrder(OrderDto orderdto)
        {
            var shippingAddress = Address.Of(orderdto.ShippingAddress.FirstName, orderdto.ShippingAddress.LastName, orderdto.ShippingAddress.EmailAddress, orderdto.ShippingAddress.AddressLine, orderdto.ShippingAddress.Country, orderdto.ShippingAddress.State, orderdto.ShippingAddress.ZipCode);  
            var billinggAddress = Address.Of(orderdto.BillinggAddress.FirstName, orderdto.BillinggAddress.LastName, orderdto.BillinggAddress.EmailAddress, orderdto.ShippingAddress.AddressLine, orderdto.BillinggAddress.Country, orderdto.BillinggAddress.State, orderdto.BillinggAddress.ZipCode);
            var newOrder = Order.Create(
                id: OrderId.Of(Guid.NewGuid()),
                customerId: CustomerId.Of(orderdto.CustomerId),
                orderName: OrderName.Of(orderdto.OrderName),
                shippingAddress: shippingAddress,
                billingAddress: billinggAddress,
                payment: Payment.Of(orderdto.Payment.CardName, orderdto.Payment.CardNumber, orderdto.Payment.Expiration, orderdto.Payment.Cvv, orderdto.Payment.PaymentMethod)
                );
            foreach (var orderItemDto in orderdto.OrderItems)
            {
                newOrder.Add(ProductId.Of(orderItemDto.ProductId), orderItemDto.Quantity, orderItemDto.Price);
            }
            return newOrder;
        }
    }
}

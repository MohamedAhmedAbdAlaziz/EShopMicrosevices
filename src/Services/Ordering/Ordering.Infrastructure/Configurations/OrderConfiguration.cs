using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ordering.Domain.ValueObjects;
using Ordering.Domain.Enums;

namespace Ordering.Infrastructure.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {

        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(oi => oi.Id).HasConversion(
            orderId => orderId.Value,
            dbId => OrderId.Of(dbId));

            builder
                .HasOne<Customer>()
                .WithMany()
                .HasForeignKey(oi => oi.CustomerId).IsRequired();


            builder
                .HasMany(x => x.OrderItems)
                .WithOne()
                .HasForeignKey(oi => oi.OrderId);
           
            
            builder.ComplexProperty(x => x.OrderName,
                namebuilder =>
                {
                    namebuilder.Property(x => x.Value)
                       .HasColumnName(nameof(Order.OrderName))
                       .HasMaxLength(100)
                       .IsRequired();
                }
                );
            builder.ComplexProperty(x => x.BillingAddress,
             namebuilder =>
             {
                 namebuilder.Property(x => x.FirstName) 
                    .HasMaxLength(50)
                    .IsRequired();

                 namebuilder.Property(x => x.LastName)
                   .HasMaxLength(50)
                   .IsRequired();

                 namebuilder.Property(x => x.EmailAddress)
                   .HasMaxLength(50)
                   .IsRequired();

                 namebuilder.Property(x => x.AddressLine)
                   .HasMaxLength(50)
                ;
                 namebuilder.Property(x => x.Country)
                   .HasMaxLength(50)
                   ;
                 namebuilder.Property(x => x.State)
                   .HasMaxLength(50)
                   ;
                 namebuilder.Property(x => x.ZipCode)
                  .HasMaxLength(50).IsRequired();
                  
             }
             );
            builder.ComplexProperty(
           x => x.ShippingAddress,
            namebuilder =>
        {
            namebuilder.Property(x => x.FirstName)
               .HasMaxLength(50)
               .IsRequired();

            namebuilder.Property(x => x.LastName)
              .HasMaxLength(50)
              .IsRequired();

            namebuilder.Property(x => x.EmailAddress)
              .HasMaxLength(50)
              .IsRequired();

            namebuilder.Property(x => x.AddressLine)
              .HasMaxLength(50)
           ;
            namebuilder.Property(x => x.Country)
              .HasMaxLength(50)
              ;
            namebuilder.Property(x => x.State)
              .HasMaxLength(50)
              ;
            namebuilder.Property(x => x.ZipCode)
             .HasMaxLength(50).IsRequired();

        }
        );
            builder.ComplexProperty(
                x => x.Payment,
                namebuilder =>
                {
                    namebuilder.Property(x => x.CardName)
                    .HasMaxLength(50);

                    namebuilder.Property(x => x.CardName)
                 .HasMaxLength(24).IsRequired();

                    namebuilder.Property(x => x.Expiration)
                 .HasMaxLength(10);
                    namebuilder.Property(x => x.CVV)
                 .HasMaxLength(3);

                    namebuilder.Property(x => x.PaymentMethod);
                 
                }
                );

            builder.Property(x => x.Status)
           .HasDefaultValue(OrderStatus.Draft)
           .HasConversion(x => x.ToString(),
               dbStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus),dbStatus)
                );

        }
    }
}

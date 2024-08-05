using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services
{
    public class DiscountService(DiscountContext dbContext, ILogger<DiscountContext> logger):DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbContext.Coupons.FirstOrDefaultAsync(x=>x.ProductName== request.ProductName);
            //return base.GetDiscount(request, context);
            if (coupon == null) 
            coupon = new Models.Coupon()
            {
                ProductName="No Discount",
                Amount= 0,
                Description ="NO Description"
            };
            logger.LogInformation("Discount is retrivede for ProductName :{productName} , Amount:{amount}", coupon.ProductName,coupon.Amount);

            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel; 
        }
        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
         var coupon= request.Coupon.Adapt<Coupon>();
            if(coupon is null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request"));
            }
            dbContext.Coupons.Add(coupon);
            await dbContext.SaveChangesAsync(); 
            logger.LogInformation("Discount is succfully Created. ProductName:{ProductName}", coupon.ProductName);
            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
            //return base.CreateDiscount(request, context);
        }
        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            if (coupon is null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request"));
            }
            dbContext.Coupons.Update(coupon);
            await dbContext.SaveChangesAsync();
            logger.LogInformation("Discount is succfully Update. ProductName:{ProductName}", coupon.ProductName);
            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
            //  return base.UpdateDiscount(request, context);
        }
        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbContext.Coupons.FirstOrDefaultAsync(x=>x.ProductName == request.ProductName);
            if (coupon is null)
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with product"));
            dbContext.Coupons.Remove(coupon);
            await dbContext.SaveChangesAsync();
            logger.LogInformation("dis count is successfully, ProductName :{ProductName}", request.ProductName);

            return new DeleteDiscountResponse { Success = true };
            //return base.DeleteDiscount(request, context);
        }
    }
}

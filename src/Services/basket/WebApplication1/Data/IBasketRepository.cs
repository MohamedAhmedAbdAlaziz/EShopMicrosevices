using Basket.API.Models;

namespace Basket.API.Data
{
    public interface IBasketRepository
    {
        Task<ShopingCart> GetBasket(string userName, CancellationToken cancellationToken);
        Task<ShopingCart> StoreBasket(ShopingCart basket, CancellationToken cancellationToken);
        Task<bool> Delete(string userName, CancellationToken cancellationToken);
    } 
}


using Basket.API.Models;
using Marten;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;


namespace Basket.API.Data
{
    public class CachedBasketRepository(IBasketRepository repository, IDistributedCache cache) : IBasketRepository
    {
        public async Task<bool> Delete(string userName, CancellationToken cancellationToken)
        {

             await repository.Delete(userName, cancellationToken);
            await cache.RemoveAsync(userName, cancellationToken);
            return true;
        }

        public async Task<ShopingCart> GetBasket(string userName, CancellationToken cancellationToken)
        {
            var cachedBasket = await cache.GetStringAsync(userName, cancellationToken);
            if (!string.IsNullOrEmpty(cachedBasket)) { 
          return   JsonSerializer.Deserialize<ShopingCart>(cachedBasket)!;
            }
            var basket= await repository.GetBasket(userName, cancellationToken);
            await cache.SetStringAsync(userName, JsonSerializer.Serialize(basket), cancellationToken);
            return basket;
        } 
        public async Task<ShopingCart> StoreBasket(ShopingCart basket, CancellationToken cancellationToken)
        {
             await repository.StoreBasket(basket, cancellationToken);
            await cache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket), cancellationToken);
            return basket;
        }
    }
}

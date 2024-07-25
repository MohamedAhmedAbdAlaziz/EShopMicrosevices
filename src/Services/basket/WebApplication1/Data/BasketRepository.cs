
using Basket.API.Models;
using Marten;

namespace Basket.API.Data
{
    public class BasketRepository(IDocumentSession session) : IBasketRepository
    {
        
        

        public async Task<bool> Delete(string userName, CancellationToken cancellationToken)
        {

            session.Delete<ShopingCart>(userName);
            await session.SaveChangesAsync(cancellationToken);
            return true;

         }

        public async Task<ShopingCart> GetBasket(string userName, CancellationToken cancellationToken)
        {
            var basket = await session.LoadAsync<ShopingCart>(userName, cancellationToken);
            return basket is null ? throw new BasketNotFoundExecption(userName) : basket;

        }

        public async Task<ShopingCart> StoreBasket(ShopingCart basket, CancellationToken cancellationToken)
        {
            session.Store(basket);
            await session.SaveChangesAsync(cancellationToken);
            return basket;
        }
    }
}

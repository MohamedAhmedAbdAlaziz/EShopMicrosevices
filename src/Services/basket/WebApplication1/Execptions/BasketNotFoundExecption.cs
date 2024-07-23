using BuildingBlocks.Exceptions;

namespace Basket.API.Execptions
{
    public class BasketNotFoundExecption : NotFoundExcetion
    {
        public BasketNotFoundExecption(string username) : base("Basket",username)
        {
        }
    }
}

namespace Basket.API.Models
{
    public class ShopingCart
    {
        public string UserName { get; set; }
        
        public List<ShopingCartItem> Items { get; set; }
        public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);

        public ShopingCart(string userName)
        {
            UserName = userName;
        }
        public ShopingCart()
        {

        }
    }
}

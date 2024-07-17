namespace Catalog.API.Exceptions
{
    public class ProdutNotFoundException:Exception
    {
        public ProdutNotFoundException():base("Product not found") {
        }
    }
}

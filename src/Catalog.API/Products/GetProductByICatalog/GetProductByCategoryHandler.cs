
namespace Catalog.API.Products.GetProductByCategory
{
    public record GetProductByCategoryQuery(string  Catalog):IQuery<GetProductByCatalogResult>;

public record GetProductByCatalogResult(IEnumerable<Product> Products);

    public class GetProductByCategoryQueryHandler(IDocumentSession session , ILogger<GetProductByCategoryQueryHandler> logger)
        : IQueryHandler<GetProductByCategoryQuery, GetProductByCatalogResult>
    {
        

        public async Task<GetProductByCatalogResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductByCatalogQueryHandler.Handler called with {@query}", query);
            var products = await session.Query<Product>().Where(x => x.Category.Contains(query.Catalog)).ToListAsync();
            if (products == null)
            {
                throw new Exception();
            }
            return new GetProductByCatalogResult(products);
        }
    }
}

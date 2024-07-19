using Marten.Linq.QueryHandlers;
using Marten.Pagination;

namespace Catalog.API.Products.GetProducts
{
    public record GetProductsQuery(int? PageNumber, int? PageSize = 5) : IQuery<GetProductResult>;

    //public record GetProductsQuery():IQuery<GetProductResult>;
    public record GetProductResult(IEnumerable<Product> Products);
    public class GetProductQueryHandler(IDocumentSession session
   //   ILogger<GetProductQueryHandler> logger
        ) : IQueryHandler<GetProductsQuery, GetProductResult>
    {
        public async Task<GetProductResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            // logger.LogInformation("GetProductsQueryHandler.Handle called with {@Query}",query);
            var products = await session.Query<Product>()
                .ToPagedListAsync(query.PageNumber ?? 1, query.PageNumber ?? 10, cancellationToken);
                          return new GetProductResult(products);
        }
    }
}

using BuildingBlocks.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Exceptions
{
    public class ProdutNotFoundException: NotFoundExcetion
    {
        public ProdutNotFoundException(Guid Id):base("Product", Id) 
        {
        }
    }
}

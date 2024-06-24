using Greggs.Products.Api.DataAccess;
using Greggs.Products.Api.Extensions;
using Greggs.Products.Api.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Greggs.Products.Api.Queries
{
    public record GetAllProductsQuery(int PageStart, int PageSize, string Currency) : IRequest<IEnumerable<ConvertedProduct>>;

    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ConvertedProduct>>
    {
        private readonly IDataAccess<Product> _productDataAccess;
        private readonly IProductConverter _productConverter;

        public GetAllProductsHandler(IDataAccess<Product> productDataAccess, IProductConverter productConverter)
        {
            _productDataAccess = productDataAccess;
            _productConverter = productConverter;
        }

        public async Task<IEnumerable<ConvertedProduct>> Handle(GetAllProductsQuery allProductsQuery, CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
                _productDataAccess.List(allProductsQuery.PageStart, allProductsQuery.PageSize)
                .Select(p => _productConverter.ToConvertedProduct(p, allProductsQuery.Currency))
            ); 
        }
    }
}

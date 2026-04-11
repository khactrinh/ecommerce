// using Dapper;
// using Ecommerce.Application.Catalog.Products.Queries.GetProductById;
// using MediatR;
// using Ecommerce.Application.Common.Interfaces;
//
// namespace Ecommerce.Application.Catalog.GetProductById;
//
// public class GetProductByIdQueryHandler 
//     : IRequestHandler<GetProductByIdQuery, ProductDetailDto?>
// {
//     private readonly IDbConnectionFactory _connectionFactory;
//
//     public GetProductByIdQueryHandler(IDbConnectionFactory connectionFactory)
//     {
//         _connectionFactory = connectionFactory;
//     }
//
//     public async Task<ProductDetailDto?> Handle(
//         GetProductByIdQuery request,
//         CancellationToken cancellationToken)
//     {
//         using var connection = _connectionFactory.CreateConnection();
//
//         var sql = @"
//             SELECT id, name, price, stock, image_url
//             FROM products
//             WHERE id = @Id
//             LIMIT 1;
//         ";
//
//         var product = await connection.QueryFirstOrDefaultAsync<ProductDetailDto>(
//             sql,
//             new { request.Id }
//         );
//
//         return product;
//     }
// }

using Ecommerce.Domain.Exceptions;
using MediatR;

namespace Ecommerce.Application.Catalog.Products.Queries.GetProductById;

public class GetProductByIdQueryHandler 
    : IRequestHandler<GetProductByIdQuery, ProductDetailResponse>
{
    
    private readonly IGetProductByIdQueryService _queryService;

    public GetProductByIdQueryHandler(IGetProductByIdQueryService queryService)
    {
        _queryService = queryService;
    }

    public async Task<ProductDetailResponse> Handle(
        GetProductByIdQuery request,
        CancellationToken cancellationToken)
    {
        var product = await _queryService.Execute(request.Id);

        if (product == null)
            throw new ProductNotFoundException(request.Id);

        return new ProductDetailResponse(
            product.Id,
            product.Name,
            product.Price,
            product.Stock,
            product.ImageUrl,
            product.CategoryId
        );
    }
}
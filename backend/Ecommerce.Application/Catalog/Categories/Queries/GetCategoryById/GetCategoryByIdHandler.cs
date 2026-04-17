using Dapper;
using Ecommerce.Domain.Exceptions;
using MediatR;
using System.Data;
using Ecommerce.Application.Catalog.Categories.Dtos;
using Ecommerce.Application.Features.Categories.Queries.GetCategories;

namespace Ecommerce.Application.Catalog.Categories.Queries.GetCategoryById;

public class GetCategoryByIdHandler 
    : IRequestHandler<GetCategoryByIdQuery, CategoryDetailResponse>
{
    private readonly IDbConnection _connection;

    public GetCategoryByIdHandler(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<CategoryDetailResponse> Handle(
        GetCategoryByIdQuery request, 
        CancellationToken cancellationToken)
    {
        var sql = @"
            SELECT id, name, slug, parent_id AS ParentId
            FROM categories
            WHERE id = @Id AND is_deleted = false
        ";

        var category = await _connection.QueryFirstOrDefaultAsync<CategoryDetailResponse>(
            sql,
            new { request.Id }
        );

        if (category == null)
            throw new CategoryNotFoundException(request.Id);

        return category;
    }
}
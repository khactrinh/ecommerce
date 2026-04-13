using Dapper;
using Ecommerce.Domain.Exceptions;
using MediatR;
using System.Data;
using Ecommerce.Application.Features.Categories.Queries.GetCategories;
using Ecommerce.Application.Features.Categories.Queries.GetCategoryById;

namespace Ecommerce.Application.Catalog.Categories.Queries.GetCategoryById;

public class GetCategoryByIdHandler 
    : IRequestHandler<GetCategoryByIdQuery, CategoryDto>
{
    private readonly IDbConnection _connection;

    public GetCategoryByIdHandler(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<CategoryDto> Handle(
        GetCategoryByIdQuery request, 
        CancellationToken cancellationToken)
    {
        var sql = @"
            SELECT id, name, slug, parent_id AS ParentId
            FROM categories
            WHERE id = @Id AND is_deleted = false
        ";

        var category = await _connection.QueryFirstOrDefaultAsync<CategoryDto>(
            sql,
            new { request.Id }
        );

        if (category == null)
            throw new CategoryNotFoundException(request.Id);

        return category;
    }
}
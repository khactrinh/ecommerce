using Dapper;
using Ecommerce.Application.Common.Interfaces;

namespace Ecommerce.Infrastructure.ReadModels.Categories;

public class CategoryQueryService : ICategoryQueryService
{
    private readonly IDbConnectionFactory _connectionFactory;

    public CategoryQueryService(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<List<Guid>> GetDescendantIds(Guid categoryId)
    {
        using var connection = _connectionFactory.CreateConnection();

        var sql = @"
            WITH RECURSIVE category_tree AS (
                SELECT id
                FROM categories
                WHERE id = @CategoryId

                UNION ALL

                SELECT c.id
                FROM categories c
                INNER JOIN category_tree ct ON c.parent_id = ct.id
                WHERE c.is_deleted = false
            )
            SELECT id FROM category_tree;
        ";

        var result = await connection.QueryAsync<Guid>(sql, new { CategoryId = categoryId });

        return result.ToList();
    }
}
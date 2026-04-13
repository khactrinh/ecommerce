// using System.Data;
// using Dapper;
// using MediatR;
//
// namespace Ecommerce.Application.Catalog.Categories.Queries.GetCategoryTree;
//
// public class GetCategoryTreeHandler 
//     : IRequestHandler<GetCategoryTreeQuery, List<CategoryTreeResponse>>
// {
//     private readonly IDbConnection _connection;
//
//     public GetCategoryTreeHandler(IDbConnection connection)
//     {
//         _connection = connection;
//     }
//
//     public async Task<List<CategoryTreeResponse>> Handle(
//         GetCategoryTreeQuery request,
//         CancellationToken cancellationToken)
//     {
//         var sql = @"
//             SELECT id, name, parent_id
//             FROM categories
//             WHERE is_deleted = false
//         ";
//
//         var categories = (await _connection.QueryAsync<dynamic>(sql)).ToList();
//
//         var lookup = categories.ToLookup(x => (Guid?)x.parent_id);
//
//         List<CategoryTreeResponse> Build(Guid? parentId)
//         {
//             return lookup[parentId]
//                 .Select(x => new CategoryTreeResponse
//                 {
//                     Id = x.id,
//                     Name = x.name,
//                     Children = Build((Guid?)x.id)
//                 }).ToList();
//         }
//
//         return Build(null);
//     }
// }

using Dapper;
using MediatR;
using System.Data;

namespace Ecommerce.Application.Catalog.Categories.Queries.GetCategoryTree;

public class GetCategoryTreeHandler 
    : IRequestHandler<GetCategoryTreeQuery, List<CategoryTreeResponse>>
{
    private readonly IDbConnection _connection;

    public GetCategoryTreeHandler(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<List<CategoryTreeResponse>> Handle(
        GetCategoryTreeQuery request,
        CancellationToken cancellationToken)
    {
        var sql = @"
            SELECT id, name, slug, parent_id AS ParentId
            FROM categories
            WHERE is_deleted = false
        ";

        var categories = (await _connection
                .QueryAsync<CategoryTreeResponse>(sql))
            .ToList();

        // Build tree
        var lookup = categories.ToLookup(x => x.ParentId);

        List<CategoryTreeResponse> Build(Guid? parentId)
        {
            return lookup[parentId]
                .Select(x => new CategoryTreeResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    Slug = x.Slug,
                    ParentId = x.ParentId,
                    Children = Build(x.Id)
                })
                .ToList();
        }

        return Build(null);
    }
}
using Ecommerce.Application.Catalog.Products.Queries.GetProducts;

namespace Ecommerce.Application.Common.Caching;

public static class CacheKeys
{
    public static string ProductList(ProductFilter f)
    {
        return $"products:" +
               $"cat:{f.CategoryId}:" +
               $"search:{f.Search}:" +
               $"price:{f.MinPrice}-{f.MaxPrice}:" +
               $"sort:{f.SortBy}_{f.SortDirection}:" +
               $"page:{f.Page}_{f.PageSize}";
    }

    public static string ProductById(Guid id)
        => $"product:{id}";

    public static string CategoryTree(Guid id)
        => $"category_tree:{id}";
}
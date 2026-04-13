namespace Ecommerce.Application.Catalog.Categories.Queries.GetCategories;

public class CategoryFilter
{
    public string? Search { get; set; }
    public Guid? ParentId { get; set; }

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
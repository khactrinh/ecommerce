namespace Ecommerce.Application.Catalog.Categories.Queries.GetCategoryTree;

public class CategoryTreeResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public Guid? ParentId { get; set; }

    public List<CategoryTreeResponse> Children { get; set; } = new();
}
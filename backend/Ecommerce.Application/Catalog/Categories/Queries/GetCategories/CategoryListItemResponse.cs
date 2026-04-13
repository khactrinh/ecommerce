namespace Ecommerce.Application.Catalog.Categories.Queries.GetCategories;

public class CategoryListItemResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public Guid? ParentId { get; set; }
}
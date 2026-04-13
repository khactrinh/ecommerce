namespace Ecommerce.Application.Features.Categories.Queries.GetCategories;

public class CategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Slug { get; set; }
    public Guid? ParentId { get; set; }
}
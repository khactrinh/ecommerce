namespace Ecommerce.Application.Catalog.Categories.Dtos;

public class CategoryDetailResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Slug { get; init; } = string.Empty;
    public Guid? ParentId { get; init; }

    // 👉 thêm thông tin cha (optional)
    public string? ParentName { get; init; }

    // 👉 useful cho UI
    public bool HasChildren { get; init; }
}
namespace Ecommerce.Domain.Catalog.Categories;

public class Category
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    
    public string Slug { get; private set; }
    public Guid? ParentId { get; private set; } // tree
    public string? Description { get; private set; }
    
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    
    public bool IsDeleted { get; private set; }

    // private readonly List<Product> _products = new();
    // public IReadOnlyCollection<Product> Products => _products;

    private Category() { }

    public Category(string name, string? description, Guid? parentId = null)
    {
        Id = Guid.NewGuid();
        Name = name;
        Slug = GenerateSlug(name);
        Description = description;
        ParentId = parentId;
    }
    
    public void Update(string name, string? description)
    {
        Name = name;
        Description = description;
        Slug = GenerateSlug(name);
        UpdatedAt = DateTime.UtcNow;
    }

    public void Delete()
    {
        IsDeleted = true;
        UpdatedAt = DateTime.UtcNow;
    }
    
    private string GenerateSlug(string name)
    {
        return name.ToLower().Replace(" ", "-");
    }
}
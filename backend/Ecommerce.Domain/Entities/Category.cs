namespace Ecommerce.Domain.Entities;

public class Category
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = default!;

    private readonly List<Product> _products = new();
    public IReadOnlyCollection<Product> Products => _products;

    private Category() { }

    public Category(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }
}
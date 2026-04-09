namespace Ecommerce.Domain.Identity;

public class Role
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; }

    private Role() { }

    public Role(string name)
    {
        Name = name;
    }
}
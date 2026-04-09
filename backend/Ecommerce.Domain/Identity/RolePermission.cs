namespace Ecommerce.Domain.Identity;

public class RolePermission
{
    public Guid RoleId { get; private set; }
    public string Permission { get; private set; }

    public RolePermission(Guid roleId, string permission)
    {
        RoleId = roleId;
        Permission = permission;
    }
}
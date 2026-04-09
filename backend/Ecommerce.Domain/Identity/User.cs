namespace Ecommerce.Domain.Identity;

public class User
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    public string Email { get; private set; } = default!;
    public string PasswordHash { get; private set; } = default!;

    // 🔒 Backing field (DDD)
    private readonly List<UserRole> _userRoles = new();

    // 👀 Expose read-only
    public IReadOnlyCollection<UserRole> UserRoles => _userRoles.AsReadOnly();

    // 🔥 Convenience property (rất hữu ích)
    public IEnumerable<Role> Roles =>  _userRoles
        .Select(x => x.Role)
        .OfType<Role>();

    private User() { }

    // =========================
    // 🏗️ Factory
    // =========================
    public static User Create(string email, string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required");

        return new User
        {
            Email = email,
            PasswordHash = passwordHash
        };
    }

    // =========================
    // 🔐 Behavior
    // =========================
    public bool VerifyPassword(string password)
    {
        if (string.IsNullOrEmpty(password))
            return false;
        return BCrypt.Net.BCrypt.Verify(password, PasswordHash);
    }

    // =========================
    // 🔑 Role Management
    // =========================
    public void AddRole(Guid roleId)
    {
        if (_userRoles.Any(x => x.RoleId == roleId))
            return; // tránh duplicate

        _userRoles.Add(new UserRole(Id, roleId));
    }

    public void RemoveRole(Guid roleId)
    {
        var role = _userRoles.FirstOrDefault(x => x.RoleId == roleId);
        if (role != null)
        {
            _userRoles.Remove(role);
        }
    }

    public bool HasRole(string roleName)
    {
        return Roles.Any(r => 
            r.Name.Equals(roleName, StringComparison.OrdinalIgnoreCase));
    }
}
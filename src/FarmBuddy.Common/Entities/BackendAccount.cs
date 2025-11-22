namespace FarmBuddy.Common.Entities;

public class BackendAccount
{
    public int Id { get; set; }

    public required string Username { get; set; }

    public string PasswordHash { get; set; } = null!;

    public string? DisplayName { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime? LastLoginTime { get; set; }

    public DateTime CreateTime { get; set; } = DateTime.UtcNow;
}

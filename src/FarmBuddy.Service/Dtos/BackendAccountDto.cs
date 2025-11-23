using FarmBuddy.Common.Models;

namespace FarmBuddy.Service.Dtos;

/// <summary>
/// 後端帳戶創建輸入DTO
/// </summary>
public class CreateBackendAccountInputDto
{
    public string Username { get; set; }

    public string PasswordHash { get; set; }

    public string? DisplayName { get; set; }
}

/// <summary>
/// 後端帳戶更新輸入DTO
/// </summary>
public class UpdateBackendAccountInputDto
{
    public string? DisplayName { get; set; }

    public bool? IsActive { get; set; }
}

/// <summary>
/// 後端帳戶輸出DTO
/// </summary>
public class BackendAccountDto
{
    public int Id { get; set; }

    public string Username { get; set; }

    public string? DisplayName { get; set; }

    public bool IsActive { get; set; }

    public DateTime? LastLoginTime { get; set; }

    public DateTime CreateTime { get; set; }
}

public class QueryBackendAccountDto : PagingQueryBase
{
    public string? DisplayName { get; set; }
    public string? Username { get; set; }
}

/// <summary>
/// 登入輸入DTO
/// </summary>
public class LoginInputDto
{
    public string Username { get; set; }

    public string Password { get; set; }
}

/// <summary>
/// 登入輸出DTO
/// </summary>
public class LoginOutputDto
{
    public int UserId { get; set; }

    public string Username { get; set; }

    public string? DisplayName { get; set; }

    public string Token { get; set; }

    public DateTime ExpiresAt { get; set; }
}

/// <summary>
/// 刷新Token輸入DTO
/// </summary>
public class RefreshTokenInputDto
{
    public string Token { get; set; }
}

/// <summary>
/// 刷新Token輸出DTO
/// </summary>
public class RefreshTokenOutputDto
{
    public string Token { get; set; }

    public DateTime ExpiresAt { get; set; }
}

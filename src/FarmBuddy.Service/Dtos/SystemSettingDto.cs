using FarmBuddy.Common.Models;

namespace FarmBuddy.Service.Dtos;

/// <summary>
/// 系統設定輸出DTO
/// </summary>
public class SystemSettingDto
{
    public int Id { get; set; }

    public string Key { get; set; }

    public string Value { get; set; }

    public string? Description { get; set; }
}

/// <summary>
/// 系統設定更新輸入DTO
/// </summary>
public class UpdateSystemSettingInputDto
{
    public string? Value { get; set; }

    public string? Description { get; set; }
}

/// <summary>
/// 系統設定查詢DTO
/// </summary>
public class QuerySystemSettingDto : PagingQueryBase
{
    public string? Key { get; set; }

    public string? Value { get; set; }
}

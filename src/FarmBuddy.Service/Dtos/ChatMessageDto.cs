using FarmBuddy.Common.Enums;
using FarmBuddy.Common.Models;

namespace FarmBuddy.Service.Dtos;

/// <summary>
/// 聊天消息输出DTO
/// </summary>
public class ChatMessageDto
{
    public int Id { get; set; }

    public string UserId { get; set; }

    public ChatRoleType Role { get; set; }

    public string Content { get; set; }

    public DateTime CreateTime { get; set; }
}

/// <summary>
/// 查询聊天消息DTO
/// </summary>
public class QueryChatMessageDto : PagingQueryBase
{
    /// <summary>
    /// 用户ID
    /// </summary>
    public string? UserId { get; set; }

    /// <summary>
    /// 角色类型
    /// </summary>
    public ChatRoleType? Role { get; set; }
}

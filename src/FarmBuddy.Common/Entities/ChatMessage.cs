using System;
using FarmBuddy.Common.Enums;

namespace FarmBuddy.Common.Entities
{
    public class ChatMessage
    {
        public int Id { get; set; }

        // 用戶 ID (對應 Line UserId)
        public string UserId { get; set; }

        // 角色：User (用戶) 或 Assistant (AI)
        public ChatRoleType Role { get; set; }

        // 內容
        public string Content { get; set; }

        // 建立時間 (用於排序)
        public DateTime CreateTime { get; set; }
    }
}

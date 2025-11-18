using FarmBuddy.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmBuddy.Repository.EntityConfiguration;

public class ChatMessageConfiguration: IEntityTypeConfiguration<ChatMessage>
{
    public void Configure(EntityTypeBuilder<ChatMessage> builder)
    {
        builder.ToTable("ChatMessages", t => t.HasComment("聊天紀錄"));

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasComment("主鍵");

        builder.Property(x => x.UserId)
            .IsRequired()
            .HasComment("用戶ID (對應 Line UserId)");

        builder.Property(x => x.Role)
            .IsRequired()
            .HasComment("角色：User (用戶) 或 Assistant (AI)");

        builder.Property(x => x.Content)
            .IsRequired()
            .HasComment("聊天內容");

        builder.Property(x => x.CreateTime)
            .IsRequired()
            .HasComment("建立時間 (用於排序)");

        builder.HasIndex(x => x.UserId)
            .HasName("IX_ChatMessage_UserId");
    }
}

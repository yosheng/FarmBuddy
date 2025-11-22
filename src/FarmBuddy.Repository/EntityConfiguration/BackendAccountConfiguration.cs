using FarmBuddy.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmBuddy.Repository.EntityConfiguration;

public class BackendAccountConfiguration : IEntityTypeConfiguration<BackendAccount>
{
    public void Configure(EntityTypeBuilder<BackendAccount> builder)
    {
        builder.ToTable("BackendAccounts", t => t.HasComment("後端帳戶"));

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasComment("主鍵");

        builder.Property(x => x.Username)
            .IsRequired()
            .HasMaxLength(50)
            .HasComment("使用者名稱");

        builder.Property(x => x.PasswordHash)
            .IsRequired()
            .HasMaxLength(256)
            .HasComment("密碼雜湊值");

        builder.Property(x => x.DisplayName)
            .HasMaxLength(50)
            .HasComment("顯示名稱");

        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasDefaultValue(true)
            .HasComment("是否啟用");

        builder.Property(x => x.LastLoginTime)
            .HasComment("最後登入時間");

        builder.Property(x => x.CreateTime)
            .IsRequired()
            .HasDefaultValueSql("SYSDATETIME()")
            .HasComment("建立時間");

        builder.HasIndex(x => x.Username)
            .IsUnique()
            .HasName("IX_BackendAccount_Username");
    }
}

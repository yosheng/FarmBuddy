using FarmBuddy.Common.Entities;
using FarmBuddy.Common.Enums;
using Microsoft.EntityFrameworkCore;

namespace FarmBuddy.Repository;

public class FarmBuddyDbContext : DbContext
{
    public FarmBuddyDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<ChatMessage> ChatMessages { get; set; }

    public DbSet<BackendAccount> BackendAccounts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FarmBuddyDbContext).Assembly);

        // 生成AI模型類型的描述
        var aiModelTypeDescription = GenerateAiModelTypeDescription();

        modelBuilder.Entity<SystemSetting>().HasData(
            new SystemSetting { Id = 1, Key = "Endpoint:CwaApi", Value = "https://opendata.cwa.gov.tw/api" },
            new SystemSetting { Id = 2, Key = "Endpoint:CwaApiKey", Value = "氣象開放資料平台會員授權碼" },
            new SystemSetting { Id = 3, Key = "OpenAIOption:ChatModelId", Value = "gpt-4" },
            new SystemSetting { Id = 4, Key = "OpenAIOption:ApiKey", Value = "YOUR_API_KEY" },
            new SystemSetting { Id = 5, Key = "GeminiOption:ChatModelId", Value = "gemini-2.5-flash" },
            new SystemSetting { Id = 6, Key = "GeminiOption:ApiKey", Value = "YOUR_API_KEY" },
            new SystemSetting
            {
                Id = 7, Key = "KernelConfig:AiModelType", Value = "0", Description = $"AI類型: {aiModelTypeDescription}"
            },
            new SystemSetting { Id = 8, Key = "LineConfig:ChannelId", Value = "YOUR_CHANNEL_ID" },
            new SystemSetting { Id = 9, Key = "LineConfig:ChannelSecret", Value = "YOUR_CHANNEL_SECRET" },
            new SystemSetting
                { Id = 10, Key = "KernelConfig:SystemMessage", Value = "YOUR_SystemMessage", Description = "系統提示詞" },
            new SystemSetting
            {
                Id = 11, Key = "KernelConfig:AssistantMessage", Value = "YOUR_AssistantMessage", Description = "助手提示詞"
            },
            new SystemSetting
            {
                Id = 12, Key = "JwtConfig:Issuer", Value = "farm-buddy-api", Description = "JWT頒發者"
            },
            new SystemSetting
            {
                Id = 13, Key = "JwtConfig:Audience", Value = "farm-buddy-api", Description = "JWT客戶端"
            },
            new SystemSetting
            {
                Id = 14, Key = "JwtConfig:Key", Value = "u5w/8x+A1b2C3d4E5f6G7h8I9j0K1l2M3n4O5p6Q7r8=", Description = "JWT密鑰"
            },
            new SystemSetting() { Id = 15, Key = "Endpoint:MoaApiKey", Value = "農業開放資料平台會員授權碼" },
            new SystemSetting() { Id = 16, Key = "Endpoint:MoaApi", Value = "https://data.moa.gov.tw/api/v1" }
        );
    }

    private static string GenerateAiModelTypeDescription()
    {
        var descriptions = new List<string>();

        foreach (var value in Enum.GetValues(typeof(AiModelType)))
        {
            var enumValue = (AiModelType)value;
            var enumName = enumValue.ToString();
            var enumNumber = (int)value;
            descriptions.Add($"{enumNumber}={enumName}");
        }

        return string.Join(", ", descriptions);
    }
}
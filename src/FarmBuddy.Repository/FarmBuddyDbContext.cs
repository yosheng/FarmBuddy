using FarmBuddy.Common.Entities;
using FarmBuddy.Common.Enums;
using Microsoft.EntityFrameworkCore;

namespace FarmBuddy.Repository;

public class FarmBuddyDbContext : DbContext
{
    public FarmBuddyDbContext(DbContextOptions options) : base(options)
    {
    }
    
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
            new SystemSetting { Id = 7, Key = "AiModelType", Value = "0", Description = $"AI類型: {aiModelTypeDescription}"}
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
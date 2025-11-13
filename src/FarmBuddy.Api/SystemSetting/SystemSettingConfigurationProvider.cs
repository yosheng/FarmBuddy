namespace FarmBuddy.Api.SystemSetting;

public sealed class SystemSettingConfigurationProvider : ConfigurationProvider
{
    private readonly string? _connectionString;

    public SystemSettingConfigurationProvider(string? connectionString)
    {
        _connectionString = connectionString;
    }

    public override void Load()
    {
        using var dbContext = new SystemSettingDbContext(_connectionString);

        dbContext.Database.EnsureCreated();

        Data = (dbContext.SystemSetting.Any()
            ? dbContext.SystemSetting.ToDictionary(
                static c => c.Key,
                static c => c.Value)
            : new Dictionary<string, string>())!;
    }
}
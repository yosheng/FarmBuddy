using FarmBuddy.Repository.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace FarmBuddy.Api.SystemSetting;

public sealed class SystemSettingDbContext : DbContext
{
    private readonly string? _connectionString;

    public SystemSettingDbContext(string? connectionString)
    {
        _connectionString = connectionString;
    }

    public DbSet<FarmBuddy.Common.Entities.SystemSetting> SystemSetting => Set<FarmBuddy.Common.Entities.SystemSetting>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new SystemSettingConfiguration());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionString);
    }
}
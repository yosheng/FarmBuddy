using FarmBuddy.Common.Entities;
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

        modelBuilder.Entity<SystemSetting>().HasData(
            new SystemSetting { Id = 1, Key = "Endpoint:CwaApi", Value = "https://opendata.cwa.gov.tw/api" },
            new SystemSetting { Id = 2, Key = "Endpoint:CwaApiKey", Value = "氣象開放資料平台會員授權碼" }
        );
    }
}
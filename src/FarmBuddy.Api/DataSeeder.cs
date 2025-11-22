using FarmBuddy.Common.Entities;
using FarmBuddy.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FarmBuddy.Api;

public class DataSeeder
{
    private readonly FarmBuddyDbContext _dbContext;
    private readonly IPasswordHasher<BackendAccount> _passwordHasher;

    public DataSeeder(FarmBuddyDbContext dbContext, IPasswordHasher<BackendAccount> passwordHasher)
    {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
    }

    public async Task SeedAsync()
    {
        if (await _dbContext.BackendAccounts.AnyAsync())
        {
            return;
        }

        var adminUser = new BackendAccount()
        {
            Username = "admin",
            DisplayName = "管理員"
        };
        adminUser.PasswordHash = _passwordHasher.HashPassword(adminUser, "farmbuddy");
            
        _dbContext.BackendAccounts.Add(adminUser);
        await _dbContext.SaveChangesAsync();
    }
}
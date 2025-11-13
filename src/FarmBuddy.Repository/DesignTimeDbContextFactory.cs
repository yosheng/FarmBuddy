using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FarmBuddy.Repository;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<FarmBuddyDbContext>
{
    public FarmBuddyDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<FarmBuddyDbContext>();
        optionsBuilder
            .UseSqlServer("Server=127.0.0.1,1401;Database=FarmBuddy;User Id=sa;Password=StrongP@ssw0rd!;Trusted_Connection=False;TrustServerCertificate=true");

        return new FarmBuddyDbContext(optionsBuilder.Options);
    }
}
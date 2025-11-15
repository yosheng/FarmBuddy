using DotNetEnv;
using FarmBuddy.Api.SystemSetting;
using FarmBuddy.Repository;
using FarmBuddy.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Xunit.DependencyInjection.Logging;

namespace FarmBuddy.Api.Tests;

public class Startup
{
    public void ConfigureHost(IHostBuilder hostBuilder)
    {
        Env.Load();

        var connectionString = Env.GetString("ConnectionString");
        if (string.IsNullOrEmpty(connectionString))
        {
            connectionString =
                "Server=127.0.0.1,1401;Database=FarmBuddy;User Id=sa;Password=StrongP@ssw0rd!;Trusted_Connection=False;TrustServerCertificate=true";
        }
        
        hostBuilder
            .ConfigureAppConfiguration(builder =>
            {
                // 注册配置
                builder
                    .AddInMemoryCollection(new Dictionary<string, string>()
                    {
                        {
                            "ConnectionString",
                            connectionString
                        }
                    }!)
                    .AddJsonFile("appsettings.json")
                    .Add(new SystemSettingConfigurationSource(
                        connectionString));
            })
            .ConfigureServices((context, services) =>
            {
                services.AddLogging(lb => lb.AddXunitOutput(options =>
                {
                    options.Filter = (category, logLevel) =>
                    {
                        if (category != null &&
                            category.Contains("Microsoft.EntityFrameworkCore.Database.Command"))
                        {
                            return logLevel >= LogLevel.Warning;
                        }

                        return true; // 其他 category 全都允許
                    };
                }));

                services.AddDbContext<FarmBuddyDbContext>(options =>
                    options.UseSqlServer(context.Configuration["ConnectionString"])
                );

                services.AddOpenAiConfiguration(context.Configuration);
                services.AddServices(context.Configuration);
            });
    }
}
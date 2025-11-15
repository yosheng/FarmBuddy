using FarmBuddy.Api;
using FarmBuddy.Repository;
using FarmBuddy.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddSystemSettingConfiguration();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "FarmBuddy API",
        Version = "v1",
        Description = "農小秘後端API",
        Contact = new OpenApiContact { Name = "FarmBuddy Team" }
    });

    Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.xml").ToList().ForEach(file =>
    {
        options.IncludeXmlComments(file, true);
    });
});

builder.Services.AddDbContext<FarmBuddyDbContext>(options =>
    options.UseSqlServer(builder.Configuration["ConnectionString"])
);

builder.Services.AddOpenAiConfiguration(builder.Configuration);
builder.Services.AddServices(builder.Configuration);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var dbContext = services.GetRequiredService<FarmBuddyDbContext>();

        // 執行遷移
        dbContext.Database.Migrate();

        Console.WriteLine("Database migrations applied successfully.");
    }
    catch (Exception ex)
    {
        // 處理遷移過程中可能發生的錯誤
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while applying database migrations.");
        // 在生產環境中，您可能需要更完善的錯誤處理機制
        // 例如：發送警報、重試或優雅地停止應用程式
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
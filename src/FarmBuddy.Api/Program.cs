using System.Net.Mime;
using FarmBuddy.Api;
using FarmBuddy.Api.Filters;
using FarmBuddy.Api.Middleware;
using FarmBuddy.Common.Authentication;
using FarmBuddy.Repository;
using FarmBuddy.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddSystemSettingConfiguration();

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ProducesAttribute(MediaTypeNames.Application.Json));
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();

    options.Filters.Add(new AuthorizeFilter(policy));
    options.Filters.Add(new ApiResponseWrapperFilter());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.OperationFilter<ApiResponseOperationFilter>();
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
    
    // 定義安全傳輸方案
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "請直接輸入您的 JWT Token (不需要輸入 'Bearer ' 前綴，Swagger 會自動加上)"
    });

    // 定義安全需求
    options.OperationFilter<SecurityOperationFilter>();
});

builder.Services.AddDbContext<FarmBuddyDbContext>(options =>
    options.UseSqlServer(builder.Configuration["ConnectionString"])
);

builder.Services.AddOpenAiConfiguration(builder.Configuration);
builder.Services.AddServices(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddScoped<DataSeeder>();

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
        
        // 從 Scope 中獲取 Seeder
        var seeder = services.GetRequiredService<DataSeeder>();
        
        // 執行初始化
        await seeder.SeedAsync();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while initial database.");
    }
}

// Configure the HTTP request pipeline.
// 异常处理必须最先注册，以捕获后续所有异常
app.UseExceptionHandling();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
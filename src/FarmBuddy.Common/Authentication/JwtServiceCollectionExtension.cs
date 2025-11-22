using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace FarmBuddy.Common.Authentication;

public static class JwtServiceCollectionExtension
{
    // 這是一個擴充方法，用來封裝複雜的 JWT 設定
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var section = configuration.GetSection(nameof(JwtConfig));
        serviceCollection.AddOptions<JwtConfig>()
            .Bind(section)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        var jwtSettings = section.Get<JwtConfig>();

        if (jwtSettings == null)
        {
            Console.WriteLine("找不到JWT配置會導致授權服務異常，請檢查配置!");
            return serviceCollection;
        }
        
        var keyBytes = Encoding.UTF8.GetBytes(jwtSettings.Key);
        
        serviceCollection.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false; // 開發環境可關閉
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience
                };
            });

        return serviceCollection;
    }
}
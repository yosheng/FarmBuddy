using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FarmBuddy.Common.Authentication;
using FarmBuddy.Common.Entities;
using FarmBuddy.Repository;
using FarmBuddy.Service.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FarmBuddy.Service.Services;

public interface IAuthService
{
    Task<LoginOutputDto> LoginAsync(LoginInputDto input);
    Task<RefreshTokenOutputDto> RefreshTokenAsync(RefreshTokenInputDto input);
}

public class AuthService : IAuthService
{
    private readonly FarmBuddyDbContext _dbContext;
    private readonly IPasswordHasher<BackendAccount> _passwordHasher;
    private readonly IOptions<JwtConfig> _jwtConfig;

    public AuthService(FarmBuddyDbContext dbContext, IPasswordHasher<BackendAccount> passwordHasher, IOptions<JwtConfig> jwtConfig)
    {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
        _jwtConfig = jwtConfig;
    }

    public async Task<LoginOutputDto> LoginAsync(LoginInputDto input)
    {
        var user = await _dbContext.BackendAccounts.FirstOrDefaultAsync(x => x.Username == input.Username);
        if (user == null || user.IsActive == false)
        {
            throw new UnauthorizedAccessException("Invalid username or password");
        }

        var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, input.Password);
        if (verificationResult == PasswordVerificationResult.Failed)
        {
            throw new UnauthorizedAccessException("Invalid username or password");
        }

        var token = GenerateJwtToken(user);
        var expiresAt = DateTime.UtcNow.AddHours(1);

        return new LoginOutputDto
        {
            UserId = user.Id,
            Username = user.Username,
            DisplayName = user.DisplayName,
            Token = token,
            ExpiresAt = expiresAt
        };
    }

    public async Task<RefreshTokenOutputDto> RefreshTokenAsync(RefreshTokenInputDto input)
    {
        var userId = ExtractUserIdFromToken(input.Token);
        if (userId == 0)
        {
            throw new UnauthorizedAccessException("Invalid token");
        }

        var user = await _dbContext.BackendAccounts.FirstOrDefaultAsync(x => x.Id == userId);
        if (user == null || user.IsActive == false)
        {
            throw new UnauthorizedAccessException("Invalid token or user inactive");
        }

        var token = GenerateJwtToken(user);
        var expiresAt = DateTime.UtcNow.AddHours(1);

        return new RefreshTokenOutputDto
        {
            Token = token,
            ExpiresAt = expiresAt
        };
    }

    private string GenerateJwtToken(BackendAccount user)
    {
        var keyBytes = Encoding.UTF8.GetBytes(_jwtConfig.Value.Key);
        var signingCredentials =
            new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Username),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _jwtConfig.Value.Issuer,
            audience: _jwtConfig.Value.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private int ExtractUserIdFromToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();

        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Value.Key)),
            ValidateIssuer = true,
            ValidIssuer = _jwtConfig.Value.Issuer,
            ValidateAudience = true,
            ValidAudience = _jwtConfig.Value.Audience,
            ValidateLifetime = false
        };

        try
        {
            var principal = handler.ValidateToken(token, validationParameters, out _);
            var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier);

            return userIdClaim != null && int.TryParse(userIdClaim.Value, out var userId) ? userId : 0;
        }
        catch
        {
            return 0;
        }
    }
}

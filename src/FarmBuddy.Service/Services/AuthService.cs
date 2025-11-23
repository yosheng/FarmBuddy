using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using FarmBuddy.Common.Authentication;
using FarmBuddy.Common.Context;
using FarmBuddy.Common.Entities;
using FarmBuddy.Common.Exceptions;
using FarmBuddy.Common.Response;
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
    Task<BackendAccountDto> GetMeAsync();
}

public class AuthService : IAuthService
{
    private readonly FarmBuddyDbContext _dbContext;
    private readonly IPasswordHasher<BackendAccount> _passwordHasher;
    private readonly IOptions<JwtConfig> _jwtConfig;
    private readonly ApiRequestContext _apiRequestContext;
    private readonly IMapper _mapper;

    public AuthService(FarmBuddyDbContext dbContext, IPasswordHasher<BackendAccount> passwordHasher,
        IOptions<JwtConfig> jwtConfig, ApiRequestContext apiRequestContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
        _jwtConfig = jwtConfig;
        _apiRequestContext = apiRequestContext;
        _mapper = mapper;
    }

    public async Task<LoginOutputDto> LoginAsync(LoginInputDto input)
    {
        var user = await _dbContext.BackendAccounts.FirstOrDefaultAsync(x => x.Username == input.Username);
        if (user == null || user.IsActive == false)
        {
            throw new BusinessException(ErrorCode.ValidationError, "錯誤的帳號或密碼");
        }

        var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, input.Password);
        if (verificationResult == PasswordVerificationResult.Failed)
        {
            throw new BusinessException(ErrorCode.ValidationError, "錯誤的帳號或密碼");
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
            throw new BusinessException(ErrorCode.Unauthorized, "Invalid token");
        }

        var user = await _dbContext.BackendAccounts.FirstOrDefaultAsync(x => x.Id == userId);
        if (user == null || user.IsActive == false)
        {
            throw new BusinessException(ErrorCode.Unauthorized, "Invalid token or user inactive");
        }

        var token = GenerateJwtToken(user);
        var expiresAt = DateTime.UtcNow.AddHours(1);

        return new RefreshTokenOutputDto
        {
            Token = token,
            ExpiresAt = expiresAt
        };
    }

    public async Task<BackendAccountDto> GetMeAsync()
    {
        var userId = int.Parse(_apiRequestContext.UserId!);
        var account = await _dbContext.BackendAccounts.FirstOrDefaultAsync(x => x.Id == userId);
        if (account == null)
        {
            throw new BusinessException(ErrorCode.NotFound ,$"Backend account with id {userId} not found");
        }
        return _mapper.Map<BackendAccountDto>(account);
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
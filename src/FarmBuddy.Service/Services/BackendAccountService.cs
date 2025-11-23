using AutoMapper;
using AutoMapper.QueryableExtensions;
using FarmBuddy.Common.Entities;
using FarmBuddy.Common.Exceptions;
using FarmBuddy.Common.Models;
using FarmBuddy.Repository;
using FarmBuddy.Service.Dtos;
using Microsoft.EntityFrameworkCore;

namespace FarmBuddy.Service.Services;

public interface IBackendAccountService
{
    Task<BackendAccountDto?> GetByIdAsync(int id);
    Task<PagingResult<BackendAccountDto>> GetPagingAsync(QueryBackendAccountDto input);
    Task<BackendAccountDto> CreateAsync(CreateBackendAccountInputDto input);
    Task<BackendAccountDto> UpdateAsync(int id, UpdateBackendAccountInputDto input);
    Task DeleteAsync(int id);
}

public class BackendAccountService : IBackendAccountService
{
    private readonly FarmBuddyDbContext _dbContext;
    private readonly IMapper _mapper;

    public BackendAccountService(FarmBuddyDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<BackendAccountDto?> GetByIdAsync(int id)
    {
        return await _dbContext.BackendAccounts
            .Where(x => x.Id == id)
            .ProjectTo<BackendAccountDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
    }

    public async Task<PagingResult<BackendAccountDto>> GetPagingAsync(QueryBackendAccountDto input)
    {
        var query = _dbContext.BackendAccounts
            .WhereIf(!string.IsNullOrWhiteSpace(input.DisplayName),
                x => x.DisplayName!.Contains(input.DisplayName!))
            .WhereIf(!string.IsNullOrWhiteSpace(input.Username), 
                x => x.Username.Contains(input.Username!));

        return await query
            .ProjectTo<BackendAccountDto>(_mapper.ConfigurationProvider)
            .ToPagingResultAsync(input);
    }

    public async Task<BackendAccountDto> CreateAsync(CreateBackendAccountInputDto input)
    {
        var account = new BackendAccount
        {
            Username = input.Username,
            PasswordHash = input.PasswordHash,
            DisplayName = input.DisplayName,
            IsActive = true,
            CreateTime = DateTime.UtcNow
        };

        _dbContext.BackendAccounts.Add(account);
        await _dbContext.SaveChangesAsync();

        return _mapper.Map<BackendAccountDto>(account);
    }

    public async Task<BackendAccountDto> UpdateAsync(int id, UpdateBackendAccountInputDto input)
    {
        var account = await _dbContext.BackendAccounts.FirstOrDefaultAsync(x => x.Id == id);
        if (account == null)
        {
            throw new BusinessException(ErrorCode.NotFound, $"Backend account with id {id} not found");
        }

        if (input.DisplayName != null)
        {
            account.DisplayName = input.DisplayName;
        }

        if (input.IsActive.HasValue)
        {
            account.IsActive = input.IsActive.Value;
        }

        _dbContext.BackendAccounts.Update(account);
        await _dbContext.SaveChangesAsync();

        return _mapper.Map<BackendAccountDto>(account);
    }

    public async Task DeleteAsync(int id)
    {
        var account = await _dbContext.BackendAccounts.FirstOrDefaultAsync(x => x.Id == id);
        if (account == null)
        {
            throw new BusinessException(ErrorCode.NotFound, $"Backend account with id {id} not found");
        }

        _dbContext.BackendAccounts.Remove(account);
        await _dbContext.SaveChangesAsync();
    }
}
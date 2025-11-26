using AutoMapper;
using AutoMapper.QueryableExtensions;
using FarmBuddy.Common.Entities;
using FarmBuddy.Common.Exceptions;
using FarmBuddy.Common.Models;
using FarmBuddy.Repository;
using FarmBuddy.Service.Dtos;
using Microsoft.EntityFrameworkCore;

namespace FarmBuddy.Service.Services;

public interface ISystemSettingService
{
    Task<PagingResult<SystemSettingDto>> GetPagingAsync(QuerySystemSettingDto input);
    Task<SystemSettingDto> UpdateAsync(int id, UpdateSystemSettingInputDto input);
}

public class SystemSettingService : ISystemSettingService
{
    private readonly FarmBuddyDbContext _dbContext;
    private readonly IMapper _mapper;

    public SystemSettingService(FarmBuddyDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<PagingResult<SystemSettingDto>> GetPagingAsync(QuerySystemSettingDto input)
    {
        var query = _dbContext.SystemSetting
            .WhereIf(!string.IsNullOrWhiteSpace(input.Key),
                x => x.Key.Contains(input.Key!))
            .WhereIf(!string.IsNullOrWhiteSpace(input.Value),
                x => x.Value.Contains(input.Value!));

        return await query
            .ProjectTo<SystemSettingDto>(_mapper.ConfigurationProvider)
            .ToPagingResultAsync(input);
    }

    public async Task<SystemSettingDto> UpdateAsync(int id, UpdateSystemSettingInputDto input)
    {
        var setting = await _dbContext.SystemSetting.FirstOrDefaultAsync(x => x.Id == id);
        if (setting == null)
        {
            throw new BusinessException(ErrorCode.NotFound, $"System setting with id {id} not found");
        }

        if (input.Value != null)
        {
            setting.Value = input.Value;
        }

        if (input.Description != null)
        {
            setting.Description = input.Description;
        }

        _dbContext.SystemSetting.Update(setting);
        await _dbContext.SaveChangesAsync();

        return _mapper.Map<SystemSettingDto>(setting);
    }
}

using AutoMapper;
using AutoMapper.QueryableExtensions;
using FarmBuddy.Common.Entities;
using FarmBuddy.Common.Models;
using FarmBuddy.Repository;
using FarmBuddy.Service.Dtos;
using Microsoft.EntityFrameworkCore;

namespace FarmBuddy.Service.Services;

public interface IChatMessageService
{
    Task<PagingResult<ChatMessageDto>> GetPagingAsync(QueryChatMessageDto input);
}

public class ChatMessageService : IChatMessageService
{
    private readonly FarmBuddyDbContext _dbContext;
    private readonly IMapper _mapper;

    public ChatMessageService(FarmBuddyDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<PagingResult<ChatMessageDto>> GetPagingAsync(QueryChatMessageDto input)
    {
        var query = _dbContext.ChatMessages
            .WhereIf(!string.IsNullOrWhiteSpace(input.UserId),
                x => x.UserId == input.UserId)
            .WhereIf(input.Role.HasValue,
                x => x.Role == input.Role);

        return await query.OrderByDescending(x => x.CreateTime)
            .ProjectTo<ChatMessageDto>(_mapper.ConfigurationProvider)
            .ToPagingResultAsync(input);
    }
}

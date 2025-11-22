using AutoMapper;
using FarmBuddy.Common.Entities;
using FarmBuddy.Service.Dtos;

namespace FarmBuddy.Service.Profiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // BackendAccount mappings
        CreateMap<BackendAccount, BackendAccountDto>();
    }
}

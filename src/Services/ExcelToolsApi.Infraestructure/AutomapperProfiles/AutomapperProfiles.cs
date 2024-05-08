using AutoMapper;
using ExcelToolsApi.Domain.DTO;
using ExcelToolsApi.JWT.Service;

namespace ExcelToolsApi.Infraestructure.AutomapperProfiles;

public class AutomapperProfiles : Profile
{
    public AutomapperProfiles()
    {
        CreateMap<LoginRequestDTO, AuthenticationLoginAdapter>();
        CreateMap<RegisterRequestDTO, AuthenticationRegisterAdapter>();
        CreateMap<TokenRequestDTO, AuthenticationTokenRequestAdapter>();
    }
}

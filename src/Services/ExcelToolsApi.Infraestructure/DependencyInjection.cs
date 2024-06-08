using ExcelToolsApi.Domain.DTO;
using ExcelToolsApi.Infraestructure.Authentication;
using ExcelToolsApi.JWT.Service;
using Microsoft.Extensions.DependencyInjection;

namespace ExcelToolsApi.Infraestructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, Microsoft.Extensions.Configuration.ConfigurationManager configuration)
    {
        services.Configure<JwtSettingDTO>(configuration.GetSection(JwtSettingDTO.SectionName));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        return services;
    }
}

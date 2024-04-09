using Microsoft.Extensions.DependencyInjection;
using ExcelToolsApi.JWT.Service.Implementation;

namespace ExcelToolsApi.JWT.Service;

public static class DependecyInjection
{
    // Inyeccion de dependencia de este servicio, para que el program.cs quede mas limpio
    public static IServiceCollection AddJWTService(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();

        return services;
    }
}

using ExcelToolsApi.JWT.Service.Contract;
using ExcelToolsApi.JWT.Service.Implementation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ExcelToolsApi.JWT.Service;

public static class DependecyInjection
{
    // Inyeccion de dependencia de este servicio, para que el program.cs quede mas limpio
    public static IServiceCollection AddJWTService(this IServiceCollection services)
    {
        // Registra AuthenticationService como implementación de IAuthenticationService
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetEntryAssembly()));
        // Registra CreateUserCommandHandler como manejador de la solicitud AuthenticationRegisterAdapter
        //services.AddScoped<IRequestHandler<AuthenticationRegisterAdapter, AuthenticationResponse>, CreateUserCommandHandler>();

        return services;
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExcelToolsApi.Infraestructure.Extensions;
public static class ConfigureServiceContainer
{
    public static void AddSwaggerOpenAPI(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddSwaggerGen();
    }
}

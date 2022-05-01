using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services
    )
    {
        return services;
    }
}
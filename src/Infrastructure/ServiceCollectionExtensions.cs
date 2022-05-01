using System.Reflection;
using Application;
using Application.Ports;
using Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices<TStartup>(
        this IServiceCollection services, IConfiguration configuration)
    {
        // Infrastructure
        services.AddAutoMapper(typeof(TStartup), typeof(InfrastructureMappingProfile));
        services.AddMediatR(typeof(TStartup).GetTypeInfo().Assembly);

        services.AddDbContext<DatabaseContext>(options =>
            options.UseInMemoryDatabase("MyDatabase"));
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        return services;
    }

    public static IServiceCollection AddOptionsConfigurations(
        this IServiceCollection services, IConfiguration configuration)
    {
        //services.Configure<SmtpOptions>(configuration.GetSection(nameof(SmtpOptions)));
        return services;
    }


    public static IServiceCollection AddHttpClients(this IServiceCollection services)
    {
        //services.AddHttpClientWithCorrelationId<ISmsService, SmsService>()
        //    .AddTraceLogHandler((response) => !response.IsSuccessStatusCode)
        //    .UseHttpClientMetrics();

        //services.AddHttpClientWithCorrelationId<IMambuHttpClient, MambuHttpClient>()
        //    .AddTraceLogHandler((response) => !response.IsSuccessStatusCode)
        //    .UseHttpClientMetrics();
        return services;
    }
}
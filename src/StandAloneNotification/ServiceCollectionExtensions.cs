using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace StandAloneNotification;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddNotificationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<NotificationSettings>(configuration.GetSection("NotificationSettings"));
        services.AddSingleton<INotificationClient, NotificationClient>();

        return services;
    }
}

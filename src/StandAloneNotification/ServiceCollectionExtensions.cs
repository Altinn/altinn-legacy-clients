using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace StandAloneNotification;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddNotificationServices(this IServiceCollection services, IConfigurationRoot configuration)
    {
        NotificationSettings notificationSettings = new();
        configuration.GetRequiredSection("NotificationSettings").Bind(notificationSettings);

        services.AddSingleton<INotificationClient, NotificationClient>();
        services.AddSingleton(notificationSettings);

        return services;
    }
}

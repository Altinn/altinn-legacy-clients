using System.Reflection;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StandAloneNotification;
using StandAloneNotification.Exceptions;
using StandAloneNotification.Models;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", false)
            .AddUserSecrets(Assembly.GetExecutingAssembly())
            .Build();

        NotificationSettings notificationSettings = new();
        configuration.GetRequiredSection("NotificationSettings").Bind(notificationSettings);

        var serviceProvider = new ServiceCollection()
            .AddNotificationServices(configuration)
            .AddSingleton(Options.Create(notificationSettings))
            .BuildServiceProvider();

        Notification notification = new()
        {
            IsReservable = true,
            LanguageId = "no",
            ReporteeNumber = "910074431", //ReporteeNumber = "910460293",
            ReceiverEndPoints = new List<ReceiverEndPointType> { new ReceiverEndPointType("", ReceiverTransportType.Email) },
            NotificationType = "MacroTest"
        };
        
        var notificationClient = serviceProvider.GetRequiredService<INotificationClient>();

        try
        {
            await notificationClient.SendNotification(new List<Notification> { notification });
        }
        catch (NotificationException e)
        {
            Console.WriteLine(e.AltinnErrorMessage);
        }

        Console.WriteLine("Hello, World!");
    }
}

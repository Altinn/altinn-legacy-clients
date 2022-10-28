using System.Reflection;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using StandAloneNotification;
using StandAloneNotification.Models;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", false)
            .AddUserSecrets(Assembly.GetExecutingAssembly())
            .Build();

        var serviceProvider = new ServiceCollection()
            .AddNotificationServices(configuration)
            .BuildServiceProvider();

        /*
         * TODO: There exists a notification type where the text templates have no hardcoded texts.
         * => SvarutMeldingFleksibel2 = {0}{1}[2}{3}
         * This might make it possible to hide the need for notification type for the consumers of
         * this library, but it might also lead to unwanted limitations in how this client can 
         * be used.
         */

        Notification notification = new()
        {
            IsReservable = true,
            LanguageId = "no",
            ReporteeNumber = "910074431", //ReporteeNumber = "910460293",
            ReceiverEndPoints = new List<ReceiverEndPointType> { new ReceiverEndPointType("", ReceiverTransportType.Email) },
            NotificationType = "MacroTest" //NotificationType = "SvarutMeldingFleksibel2"
        };
        
        var notificationClient = serviceProvider.GetRequiredService<INotificationClient>();

        await notificationClient.SendNotification(new List<Notification> { notification });

        Console.WriteLine("Hello, World!");
    }
}

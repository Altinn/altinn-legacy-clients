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

        Notification notification = new()
        {
            IsReservable = true,
            LanguageId = "no",
            ReporteeNumber = "910074431", //ReporteeNumber = "910460293",
            ReceiverEndPoints = new List<ReceiverEndPointType> { new ReceiverEndPointType("terje.holene@digdir.no", ReceiverTransportType.Email) },
            NotificationType = "MacroTest" //NotificationType = "SvarutMeldingFleksibel2"
        };
        
        var notificationClient = serviceProvider.GetRequiredService<INotificationClient>();

        await notificationClient.SendNotification(new List<Notification> { notification });

        Console.WriteLine("Hello, World!");
    }
}

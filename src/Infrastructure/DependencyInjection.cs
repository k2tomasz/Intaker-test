using Intaker.Application.Common.Interfaces;
using Intaker.Infrastructure.Data;
using Intaker.Infrastructure.Queue;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static void AddInfrastructureServices(this IHostApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("IntakerDb");
        Guard.Against.Null(connectionString, message: "Connection string 'IntakerDb' not found.");

        builder.Services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.UseSqlServer(connectionString);
        });


        var serviceBusConnection = builder.Configuration.GetSection("AzureServiceBus");

        builder.Services.AddAzureClients(clientsBuilder =>
        {
            clientsBuilder.AddServiceBusClient(serviceBusConnection["ConnectionString"])
              .WithName("ServiceBusClient")
              .ConfigureOptions(options =>
              {
                  options.RetryOptions.Delay = TimeSpan.FromMilliseconds(50);
                  options.RetryOptions.MaxDelay = TimeSpan.FromSeconds(5);
                  options.RetryOptions.MaxRetries = 3;
              });
        });

        builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        builder.Services.AddScoped<ApplicationDbContextInitialiser>();

        builder.Services.AddScoped<IMessageReceiverService, MessageService>();
        builder.Services.AddScoped<IMessageSenderService, MessageService>();
    }
}

using System.Text;
using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Intaker.Application.Common.Interfaces;
using Intaker.Domain.Events;
using MediatR;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Logging;

namespace Intaker.Infrastructure.Queue;
public class MessageService : IMessageReceiverService, IMessageSenderService
{
    //TODO: move this to appsettings.json
    private const string QUEUE_NAME = "queue-name";
    private readonly ServiceBusClient _serviceBusClient;
    private readonly ServiceBusSender _serviceBusSender;
    private readonly ILogger<MessageService> _logger;
    private readonly IMediator _mediator;

    public MessageService(IAzureClientFactory<ServiceBusClient> serviceBusClientFactory, ILogger<MessageService> logger, IMediator mediator)
    {
        _serviceBusClient = serviceBusClientFactory.CreateClient("ServiceBusClient");
        _serviceBusSender = _serviceBusClient.CreateSender(QUEUE_NAME);
        _logger = logger;
        _mediator = mediator;
    }

    public async Task SendMessage(string message)
    {
        var encodedMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(message));
        await _serviceBusSender.SendMessageAsync(encodedMessage);
    }

    public async Task ReceiveMessage()
    {
        var options = new ServiceBusProcessorOptions()
        {
            MaxConcurrentCalls = 1
        };

        await using ServiceBusProcessor processor = _serviceBusClient.CreateProcessor(QUEUE_NAME, options);

        processor.ProcessMessageAsync += MessageHandler;
        processor.ProcessErrorAsync += ErrorHandler;

        await processor.StartProcessingAsync();
        Console.ReadKey();
    }

    //TODO: we could implement this differently to handle different types of messages
    private async Task MessageHandler(ProcessMessageEventArgs args)
    {
        string body = args.Message.Body.ToString();
        var eventMessage = JsonSerializer.Deserialize<TaskStatusUpdatedEvent>(body);
        if (eventMessage != null)
        {
            await _mediator.Publish(eventMessage);
        }
        await args.CompleteMessageAsync(args.Message);
    }

    private Task ErrorHandler(ProcessErrorEventArgs args)
    {
        _logger.LogError(args.Exception, "An error occurred processing the message");
        return Task.CompletedTask;
    }
}

using Intaker.Domain.Events;
using Microsoft.Extensions.Logging;

namespace Intaker.Application.Tasks.EventHandlers;

public class TaskCreatedEventEventHandler : INotificationHandler<TaskCreatedEvent>
{
    private readonly ILogger<TaskCreatedEventEventHandler> _logger;

    public TaskCreatedEventEventHandler(ILogger<TaskCreatedEventEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(TaskCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Intaker Event: {Event}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}

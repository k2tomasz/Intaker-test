using Intaker.Domain.Events;
using Microsoft.Extensions.Logging;

namespace Intaker.Application.Tasks.EventHandlers;

public class TaskStatusUpdatedEventHandler : INotificationHandler<TaskStatusUpdatedEvent>
{
    private readonly ILogger<TaskStatusUpdatedEventHandler> _logger;

    public TaskStatusUpdatedEventHandler(ILogger<TaskStatusUpdatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(TaskStatusUpdatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Intaker Event: {Event}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}

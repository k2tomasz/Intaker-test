using System.Text.Json;
using Intaker.Application.Common.Interfaces;
using Intaker.Domain.Enums;
using Intaker.Domain.Events;

namespace Intaker.Application.TodoItems.Commands.UpdateTaskStatus;

public record UpdateTaskStatusCommand(int Id, Status NewStatus) : IRequest;

public class UpdateTodoItemCommandHandler : IRequestHandler<UpdateTaskStatusCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IMessageSenderService _messageService;

    public UpdateTodoItemCommandHandler(IApplicationDbContext context, IMessageSenderService messageService)
    {
        _context = context;
        _messageService = messageService;
    }

    public async Task Handle(UpdateTaskStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.ToDoTasks
            .FindAsync(request.Id, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.Status = request.NewStatus;

        await _context.SaveChangesAsync(cancellationToken);

        var eventMessage = new TaskStatusUpdatedEvent(entity);
        await _messageService.SendMessage(JsonSerializer.Serialize(eventMessage));
    }
}

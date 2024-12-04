using Intaker.Application.Common.Interfaces;
using Intaker.Domain.Entities;
using Intaker.Domain.Enums;

namespace Intaker.Application.TodoItems.Commands.CreateTask;

public record CreateTaskCommand(string Name, string Description) : IRequest<int>;

public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateTaskCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var entity = new ToDoTask
        {
            Name = request.Name,
            Description = request.Description,
            Status = Status.NotStarted
        };

        _context.ToDoTasks.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}

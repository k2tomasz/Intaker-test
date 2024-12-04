using Intaker.Application.Common.Interfaces;
using Intaker.Application.Common.Mappings;

namespace Intaker.Application.TodoItems.Queries.GetTasks;

public record GetTasksQuery : IRequest<List<ToDoTaskDto>>
{
}

public class GetTodoItemsWithPaginationQueryHandler : IRequestHandler<GetTasksQuery, List<ToDoTaskDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetTodoItemsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ToDoTaskDto>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
    {
        return await _context.ToDoTasks
            .ProjectToListAsync<ToDoTaskDto>(_mapper.ConfigurationProvider);
    }
}

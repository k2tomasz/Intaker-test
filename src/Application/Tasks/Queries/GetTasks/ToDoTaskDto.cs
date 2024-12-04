using Intaker.Domain.Entities;
using Intaker.Domain.Enums;

namespace Intaker.Application.TodoItems.Queries.GetTasks;

public class ToDoTaskDto
{
    public int Id { get; init; }
    public string Name { get; init; } = null!;
    public string Description { get; init; } = null!;
    public Status Status { get; init; }
    public string? AssignedTo { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<ToDoTask, ToDoTaskDto>();
        }
    }
}

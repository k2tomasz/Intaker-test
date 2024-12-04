
namespace Intaker.Domain.Entities;

public class ToDoTask : BaseEntity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public Status Status { get; set; }
    public string? AssignedTo { get; set; }

}

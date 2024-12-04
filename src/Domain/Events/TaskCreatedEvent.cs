namespace Intaker.Domain.Events;

public class TaskCreatedEvent : BaseEvent
{
    public TaskCreatedEvent(ToDoTask task)
    {
        Task = task;
    }

    public ToDoTask Task { get; }
}

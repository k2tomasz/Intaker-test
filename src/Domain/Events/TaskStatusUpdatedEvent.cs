namespace Intaker.Domain.Events;

public class TaskStatusUpdatedEvent : BaseEvent
{
    public TaskStatusUpdatedEvent(ToDoTask task)
    {
        Task = task;
    }

    public ToDoTask Task { get; }

}

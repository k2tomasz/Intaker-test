using Intaker.Application.TodoItems.Commands.CreateTask;
using Intaker.Application.TodoItems.Commands.UpdateTaskStatus;
using Intaker.Application.TodoItems.Queries.GetTasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Intaker.Web.Endpoints;

public class Tasks : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapGet(GetTasks)
            .MapPost(CreateTask)
            .MapPatch("{id}", UpdateTaskStatus);
    }

    public async Task<Ok<List<ToDoTaskDto>>> GetTasks(ISender sender, [AsParameters] GetTasksQuery query)
    {
        var result = await sender.Send(query);

        return TypedResults.Ok(result);
    }

    public async Task<Created<int>> CreateTask(ISender sender, CreateTaskCommand command)
    {
        var id = await sender.Send(command);

        return TypedResults.Created($"/{nameof(Tasks)}/{id}", id);
    }

    public async Task<Results<NoContent, BadRequest>> UpdateTaskStatus(ISender sender, int id, UpdateTaskStatusCommand command)
    {
        if (id != command.Id) return TypedResults.BadRequest();

        await sender.Send(command);

        return TypedResults.NoContent();
    }
}

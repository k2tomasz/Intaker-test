using Intaker.Domain.Entities;

namespace Intaker.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<ToDoTask> ToDoTasks { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}

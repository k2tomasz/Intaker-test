using System.Reflection;
using Intaker.Application.Common.Interfaces;
using Intaker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Intaker.Infrastructure.Data;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<ToDoTask> ToDoTasks => Set<ToDoTask>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}

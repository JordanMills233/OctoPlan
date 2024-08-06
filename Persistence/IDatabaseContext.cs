using Microsoft.EntityFrameworkCore;
using OctoPlan.Core.Models;

namespace OctoPlan.Core.Persistence;

public interface IDatabaseContext
{
    DbSet<Project> Projects { get; set; }
    DbSet<ProjectTask> ProjectTasks { get; set; }
    DbSet<User> Users { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
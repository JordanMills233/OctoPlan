using Microsoft.EntityFrameworkCore;
using OctoPlan.Core.Models;

namespace OctoPlan.Core.Persistence;

public class DatabaseContext : DbContext, IDatabaseContext
{
    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectTask> ProjectTasks { get; set; }
    public DbSet<User> Users { get; set; }
    
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        return base.SaveChangesAsync(cancellationToken);
    }
}
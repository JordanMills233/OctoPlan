using OctoPlan.Core.Models;
using Task = System.Threading.Tasks.Task;

namespace OctoPlan.Core.Interfaces;

public interface IProjectService
{
    Task<bool> CreateProjectAsync(Project project, Guid ownerId, CancellationToken ct);
    Task<Project> GetProjectByIdAsync(Guid projectId);
    Task<IEnumerable<Project>> GetProjectsByUserAsync(Guid userId, CancellationToken ct);
    Task<bool> UpdateProjectAsync(Project project, CancellationToken ct);
    Task<bool> DeleteProjectAsync(Guid projectId, CancellationToken ct);
}
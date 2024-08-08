using OctoPlan.Core.Models;
using OctoPlan.Core.Models.Requests;

namespace OctoPlan.Core.Interfaces;

public interface IProjectService
{
    Task<bool> CreateProjectAsync(CreateProjectRequest request, CancellationToken ct);
    Task<Project> GetProjectByIdAsync(Guid projectId);
    Task<IEnumerable<Project>> GetProjectsByUserAsync(Guid userId, CancellationToken ct);
    Task<bool> UpdateProjectAsync(Project project, CancellationToken ct);
    Task<bool> DeleteProjectAsync(Guid projectId, CancellationToken ct);
}
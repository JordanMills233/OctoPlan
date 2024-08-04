using OctoPlan.Core.Models;

namespace OctoPlan.Core.Interfaces;

public interface IProjectService
{
    Task<Project> CreateProjectAsync(Project project, Guid ownerId);
    Task<Project> GetProjectByIdAsync(Guid projectId);
    Task<IEnumerable<Project>> GetProjectsByUserAsync(Guid userId);
    Task<Project> UpdateProjectAsync(Project project);
    Task<Project> DeleteProjectAsync(Guid projectId);
}
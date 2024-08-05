using OctoPlan.Core.Models;
using Task = System.Threading.Tasks.Task;

namespace OctoPlan.Core.Interfaces;

public interface IProjectService
{
    Task<Project> CreateProjectAsync(Project project, Guid ownerId);
    Task<Project> GetProjectByIdAsync(Guid projectId);
    Task<IEnumerable<Project>> GetProjectsByUserAsync(Guid userId);
    Task<Project> UpdateProjectAsync(Project project);
    Task DeleteProjectAsync(Guid projectId);
}
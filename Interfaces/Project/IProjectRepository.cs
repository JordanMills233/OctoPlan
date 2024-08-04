using OctoPlan.Core.Models;
using Task = System.Threading.Tasks.Task;

namespace OctoPlan.Core.Interfaces;

public interface IProjectRepository
{
    Task<Project> GetByIdAsync(Guid id);
    Task<IEnumerable<Project>> GetAllAsync();
    Task<IEnumerable<Project>> GetProjectsByUserAsync(Guid userId);
    Task<Project> AddAsync(Project project);
    Task<Project> UpdateAsync(Project project);
    Task DeleteAsync(Project project);
}
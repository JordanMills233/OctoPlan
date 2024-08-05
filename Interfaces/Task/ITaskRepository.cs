using Task = OctoPlan.Core.Models.Task;

namespace OctoPlan.Core.Interfaces;

public interface ITaskRepository
{
    Task<Task> GetByIdAsync(Guid taskId);
    Task<IEnumerable<Task>> GetTasksByProjectAsync(Guid projetId);
    Task<Task> UpdateAsync(Task task);
    Task<Task> AddAsync(Task task);
    System.Threading.Tasks.Task DeleteAsync(Guid taskId);
}
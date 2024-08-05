using Task = OctoPlan.Core.Models.Task;

namespace OctoPlan.Core.Interfaces;

public interface ITaskService
{
    Task<Task> CreateTaskAsync(Task task);
    Task<Task> GetTaskByIdAsync(Guid taskId);
    Task<IEnumerable<Task>> GetTasksByIdAsync(Guid projectId);
    Task<Task> UpdateTaskAsync(Task task);
    System.Threading.Tasks.Task DeleteTaskAsync(Guid taskId);
    Task<Task> AssignTaskAsync(Guid taskId, Guid userId);
}
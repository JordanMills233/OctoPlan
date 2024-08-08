using OctoPlan.Core.Models;
using OctoPlan.Core.Models.Requests;

namespace OctoPlan.Core.Interfaces;

public interface IProjectTaskService
{
    Task<bool> CreateTaskAsync(CreateTaskRequest request, CancellationToken ct);
    Task<ProjectTask> GetTaskByIdAsync(Guid taskId, CancellationToken ct);
    Task<IEnumerable<ProjectTask>> GetTasksByIdAsync(Guid projectId, CancellationToken ct);
    Task<bool> UpdateTaskAsync(ProjectTask projectTask, CancellationToken ct);
    Task<bool> DeleteTaskAsync(Guid taskId, CancellationToken ct);
    Task<bool> AssignTaskAsync(Guid taskId, Guid userId, CancellationToken ct);
}
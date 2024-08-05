using OctoPlan.Core.Enums;
using OctoPlan.Core.Interfaces;
using Task = OctoPlan.Core.Models.Task;

namespace OctoPlan.Core.Services; 

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;
    private readonly IUserRepository _userRepository;

    public TaskService(ITaskRepository taskRepository, IUserRepository userRepository)
    {
        _taskRepository = taskRepository;
        _userRepository = userRepository;
    }
    
    public async Task<Task> CreateTaskAsync(Task task)
    {
        task.TaskStatus = Status.NotStarted;

        return await _taskRepository.AddAsync(task);
    }

    public async Task<Task> GetTaskByIdAsync(Guid taskId)
    {
        var task = await _taskRepository.GetByIdAsync(taskId);
        if (task == null)
        {
            throw new Exception("Task not found");
        }

        return task;
    }

    public async Task<IEnumerable<Task>> GetTasksByIdAsync(Guid projectId)
    {
        var tasks = await _taskRepository.GetTasksByProjectAsync(projectId);

        return tasks;
    }

    public async Task<Task> UpdateTaskAsync(Task updatedTask)
    {
        var task = await _taskRepository.GetByIdAsync(updatedTask.Id);

        task.TaskStatus = updatedTask.TaskStatus;
        task.Description = updatedTask.Description;
        task.Priority = updatedTask.Priority;
        task.Title = updatedTask.Title;
        task.DueDate = updatedTask.DueDate;
        task.LastModifiedDate = DateTime.Now;
        task.AssignedToUserId = updatedTask.AssignedToUserId;

        return await _taskRepository.UpdateAsync(task);
    }

    public async System.Threading.Tasks.Task DeleteTaskAsync(Guid taskId)
    {
         await _taskRepository.DeleteAsync(taskId);
    }

    public async Task<Task> AssignTaskAsync(Guid taskId, Guid userId)
    {
        var task = await _taskRepository.GetByIdAsync(taskId);

        task.AssignedToUserId = userId;

        return await _taskRepository.UpdateAsync(task);
    }
}
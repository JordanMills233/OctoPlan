using Microsoft.EntityFrameworkCore;
using OctoPlan.Core.Enums;
using OctoPlan.Core.Interfaces;
using OctoPlan.Core.Models;
using OctoPlan.Core.Persistence;

namespace OctoPlan.Core.Services; 

public class ProjectTaskService : IProjectTaskService
{
    private readonly IDatabaseContext _dbContext;

    public ProjectTaskService(IDatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<bool> CreateTaskAsync(ProjectTask projectTask, CancellationToken ct)
    {
        try
        {
            projectTask.TaskStatus = Status.NotStarted;

            await _dbContext.ProjectTasks.AddAsync(projectTask, ct);
            
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
        
    }

    public async Task<ProjectTask> GetTaskByIdAsync(Guid taskId, CancellationToken ct)
    {
        var task = await _dbContext.ProjectTasks.FirstOrDefaultAsync(pt => pt.Id.Equals(taskId), ct);
        if (task == null)
        {
            throw new Exception("ProjectTask not found");
        }

        return task;
    }

    public async Task<IEnumerable<ProjectTask>> GetTasksByIdAsync(Guid projectId, CancellationToken ct)
    {
        var tasks = await _dbContext.ProjectTasks.Where(p => p.Id.Equals(projectId)).ToListAsync(ct);

        return tasks;
    }

    public async Task<bool> UpdateTaskAsync(ProjectTask updatedProjectTask, CancellationToken ct)
    {
        try
        {
            var task = await _dbContext.ProjectTasks.FirstOrDefaultAsync(p => p.Id.Equals(updatedProjectTask.Id), ct);

            if (task == null)
            {
                throw new Exception("Task not found");
            }

            task.TaskStatus = updatedProjectTask.TaskStatus;
            task.Description = updatedProjectTask.Description;
            task.Priority = updatedProjectTask.Priority;
            task.Title = updatedProjectTask.Title;
            task.DueDate = updatedProjectTask.DueDate;
            task.LastModifiedDate = DateTime.Now;
            task.AssignedToUserId = updatedProjectTask.AssignedToUserId;

            await _dbContext.SaveChangesAsync(ct);

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> DeleteTaskAsync(Guid taskId, CancellationToken ct)
    {
        try
        {
            var task = await _dbContext.ProjectTasks.FirstOrDefaultAsync(pt => pt.Id.Equals(taskId), ct);

            if (task == null)
            {
                throw new Exception("Task doesnt exist");
            }
            _dbContext.ProjectTasks.Remove(task);

            await _dbContext.SaveChangesAsync(ct);

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async Task<bool> AssignTaskAsync(Guid taskId, Guid userId, CancellationToken ct)
    {
        try
        {
            var task = await _dbContext.ProjectTasks.FirstOrDefaultAsync(pt => pt.Id.Equals(taskId), ct);

            if (task == null)
            {
                throw new Exception("task not found");
            }

            task.AssignedToUserId = userId;

            await _dbContext.SaveChangesAsync(ct);

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
        
    }
}
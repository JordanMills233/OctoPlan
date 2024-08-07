using Microsoft.AspNetCore.Mvc;
using OctoPlan.Core.Interfaces;
using OctoPlan.Core.Models;

namespace OctoPlan.Core.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class TaskController : ControllerBase
{
    private readonly IProjectTaskService _projectTaskService;

    public TaskController(IProjectTaskService projectTaskService)
    {
        _projectTaskService = projectTaskService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTask(Guid taskId, CancellationToken ct)
    {
        var task = await _projectTaskService.GetTaskByIdAsync(taskId, ct);

        return Ok(task);
    }

    [HttpGet("AllTasks")]
    public async Task<IActionResult> GetAll(Guid projectId, CancellationToken ct)
    {
        var tasks = await _projectTaskService.GetTasksByIdAsync(projectId, ct);

        return Ok(tasks);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask(ProjectTask task, CancellationToken ct)
    {
        await _projectTaskService.CreateTaskAsync(task, ct);

        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTask(ProjectTask task, CancellationToken ct)
    {
        await _projectTaskService.UpdateTaskAsync(task, ct);

        return Ok();
    }

    [HttpPut]
    async Task<IActionResult> AssignTask(Guid taskId, Guid userId, CancellationToken ct)
    {
        await _projectTaskService.AssignTaskAsync(taskId, userId, ct);

        return Ok();
    }
}
﻿using Microsoft.AspNetCore.Mvc;
using OctoPlan.Core.Interfaces;
using OctoPlan.Core.Models;

namespace OctoPlan.Core.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class ProjectController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpGet]
    public async Task<IActionResult> GetProjectById(Guid projectId)
    {
        var project = await _projectService.GetProjectByIdAsync(projectId);

        return Ok(project);
    }

    [HttpGet("ProjectByEmail")]
    public async Task<IActionResult> GetProjectByEmail(Guid userId, CancellationToken ct)
    {
        var project = await _projectService.GetProjectsByUserAsync(userId, ct);

        return Ok(project);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProject(Project project, Guid userId, CancellationToken ct)
    {
        await _projectService.CreateProjectAsync(project, userId, ct);

        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProject(Project project, CancellationToken ct)
    {
        await _projectService.UpdateProjectAsync(project, ct);

        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteProject(Guid projectId, CancellationToken ct)
    {
        await _projectService.DeleteProjectAsync(projectId, ct);

        return Ok();
    }
}
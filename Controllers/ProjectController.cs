﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OctoPlan.Core.Interfaces;
using OctoPlan.Core.Models;
using OctoPlan.Core.Models.Requests;

namespace OctoPlan.Core.Controllers;

[ApiController]
[Route("/[controller]/[action]")]
public class ProjectController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpGet("{projectId}")]
    public async Task<IActionResult> GetProjectById(Guid projectId)
    {
        var project = await _projectService.GetProjectByIdAsync(projectId);

        return Ok(project);
    }

    [Authorize]
    [HttpGet()]
    public async Task<IActionResult> GetProjectByEmail(string email, CancellationToken ct)
    {
        var project = await _projectService.GetProjectsByEmailAsync(email, ct);

        return Ok(project);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProject([FromBody] CreateProjectRequest request, CancellationToken ct)
    {
        await _projectService.CreateProjectAsync(request, ct);

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
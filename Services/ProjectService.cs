using Microsoft.EntityFrameworkCore;
using OctoPlan.Core.Enums;
using OctoPlan.Core.Interfaces;
using OctoPlan.Core.Models;
using OctoPlan.Core.Models.Requests;
using OctoPlan.Core.Persistence;

namespace OctoPlan.Core.Services;

public class ProjectService : IProjectService
{

    private readonly IDatabaseContext _dbContext ;

    public ProjectService(IDatabaseContext databaseContext)
    {
        _dbContext = databaseContext;
    }
    
    public async Task<bool> CreateProjectAsync(CreateProjectRequest request, CancellationToken ct)
    {
        try
        {
            var project = new Project(request);
            
            await _dbContext.Projects.AddAsync(project, ct);
            await _dbContext.SaveChangesAsync(ct);

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<Project> GetProjectByIdAsync(Guid projectId)
    {
        var project = await _dbContext.Projects.FirstOrDefaultAsync(p => p.Id.Equals(projectId));
        if (project == null)
        {
            throw new Exception($"could not find project with id: {projectId}");
        }

        return project;
    }

    public async Task<IEnumerable<Project>> GetProjectsByUserAsync(Guid userId, CancellationToken ct)
    {
        var projects = await _dbContext.Projects.Where(p => p.OwnerId.Equals(userId)).ToListAsync(ct);
        if (projects == null)
        {
            throw new Exception("No projects associated with user");
        }

        return projects;
    }

    public async Task<bool> UpdateProjectAsync(Project project, CancellationToken ct)
    {
        try
        {
            var existingProject = await _dbContext.Projects.FirstOrDefaultAsync(p => p.Id.Equals(project.Id), ct);
            if (existingProject == null)
            {
                throw new Exception("no project exists");
            }

            existingProject.Title = project.Title;
            existingProject.Description = project.Description;
            existingProject.Status = project.Status;
            existingProject.StartDate = project.StartDate;
            existingProject.EndDate = project.EndDate;

            await _dbContext.SaveChangesAsync(ct);
            
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> DeleteProjectAsync(Guid projectId, CancellationToken ct)
    {
        try
        {
            var project = await _dbContext.Projects.FirstOrDefaultAsync(p => p.Id.Equals(projectId), ct);
            if (project == null)
            {
                throw new Exception("project doesnt exist");
            }

            _dbContext.Projects.Remove(project);

            await _dbContext.SaveChangesAsync(ct);

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
        
    }
}
using OctoPlan.Core.Enums;
using OctoPlan.Core.Interfaces;
using OctoPlan.Core.Models;
using Task = System.Threading.Tasks.Task;

namespace OctoPlan.Core.Services;

public class ProjectService : IProjectService
{

    private readonly IProjectRepository _projectRepository;
    private readonly IUserRepository _userRepository;

    public ProjectService(IProjectRepository projectRepository, IUserRepository userRepository)
    {
        _projectRepository = projectRepository;
        _userRepository = userRepository;
    }
    
    public async Task<Project> CreateProjectAsync(Project project, Guid ownerId)
    {
        var owner = await _userRepository.GetByIdAsync(ownerId);
        if (owner == null)
        {
            throw new Exception($"{ownerId} not found");
        }

        project.OwnerId = ownerId;
        project.Status = Status.InProgress;

        return await _projectRepository.AddAsync(project);
    }

    public async Task<Project> GetProjectByIdAsync(Guid projectId)
    {
        var project = await _projectRepository.GetByIdAsync(projectId);
        if (project == null)
        {
            throw new Exception($"could not find project with id: {projectId}");
        }

        return project;
    }

    public async Task<IEnumerable<Project>> GetProjectsByUserAsync(Guid userId)
    {
        var projects = await _projectRepository.GetProjectsByUserAsync(userId);
        if (projects == null)
        {
            throw new Exception("No projects associated with user");
        }

        return projects;
    }

    public async Task<Project> UpdateProjectAsync(Project project)
    {
        var existingProject = await _projectRepository.GetByIdAsync(project.Id);
        if (existingProject == null)
        {
            throw new Exception("no project exists");
        }

        existingProject.Title = project.Title;
        existingProject.Description = project.Description;
        existingProject.Status = project.Status;
        existingProject.StartDate = project.StartDate;
        existingProject.EndDate = project.EndDate;
        
        return await _projectRepository.UpdateAsync(existingProject);
    }

    public async Task DeleteProjectAsync(Guid projectId)
    {
        var project = await _projectRepository.GetByIdAsync(projectId);
        if (project == null)
        {
            throw new Exception("project doesnt exist");
        }

        await _projectRepository.DeleteAsync(project);
    }
}
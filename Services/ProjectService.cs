using OctoPlan.Core.Enums;
using OctoPlan.Core.Interfaces;
using OctoPlan.Core.Models;

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

    public Task<Project> GetProjectByIdAsync(Guid projectId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Project>> GetProjectsByUserAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<Project> UpdateProjectAsync(Project project)
    {
        throw new NotImplementedException();
    }

    public Task<Project> DeleteProjectAsync(Guid projectId)
    {
        throw new NotImplementedException();
    }
}
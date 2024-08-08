using OctoPlan.Core.Enums;
using OctoPlan.Core.Models.Requests;

namespace OctoPlan.Core.Models;

public record Project
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Status Status { get; set; }
    public Guid OwnerId { get; set; }

    public Project()
    {
        
    }

    public Project(CreateProjectRequest request)
    {
        Id = Guid.NewGuid();
        Title = request.Title;
        Description = request.Description;
        StartDate = request.StartDate;
        EndDate = request.EndDate;
        Status = Status.NotStarted;
        OwnerId = request.UserId;
    }
}
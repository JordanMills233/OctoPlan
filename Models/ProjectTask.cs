using OctoPlan.Core.Enums;
using OctoPlan.Core.Interfaces;
using OctoPlan.Core.Models.Requests;

namespace OctoPlan.Core.Models;

public record ProjectTask
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Priority Priority { get; set; }
    public Status TaskStatus { get; set; }
    public DateTime DueDate { get; set; }
    public Guid ProjectId { get; set; }
    public Guid? AssignedToUserId { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime LastModifiedDate { get; set; }

    public ProjectTask()
    {
        
    }

    public ProjectTask(CreateTaskRequest request)
    {
        Id = Guid.NewGuid();
        Title = request.Title;
        Description = request.Description;
        Priority = request.Priority;
        TaskStatus = Status.NotStarted;
        DueDate = request.DueDate;
        ProjectId = request.ProjectId;
        CreatedDate = DateTime.UtcNow;
        LastModifiedDate = DateTime.UtcNow;
    }
}
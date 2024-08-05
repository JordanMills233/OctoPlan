using OctoPlan.Core.Enums;

namespace OctoPlan.Core.Interfaces;

public interface ITask
{
    Guid Id { get; set; }
    string Title { get; set; }
    string Description { get; set; }
    Status TaskStatus { get; set; }
    Priority Priority { get; set; }
    DateTime DueDate { get; set; }
    Guid ProjectId { get; set; }
    Guid? AssignedToUserId { get; set; }
    DateTime CreatedDate { get; set; }
    DateTime LastModifiedDate { get; set; }
}

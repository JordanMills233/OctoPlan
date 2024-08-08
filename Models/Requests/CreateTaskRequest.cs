using OctoPlan.Core.Enums;

namespace OctoPlan.Core.Models.Requests;

public record CreateTaskRequest(string Title, string Description, Priority Priority, DateTime DueDate, Guid ProjectId);
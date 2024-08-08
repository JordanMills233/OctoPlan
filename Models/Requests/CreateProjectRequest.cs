namespace OctoPlan.Core.Models.Requests;

public record CreateProjectRequest(string Title, string Description, DateTime StartDate, DateTime EndDate, Guid UserId);
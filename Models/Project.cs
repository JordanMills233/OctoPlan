using OctoPlan.Core.Enums;
using OctoPlan.Core.Interfaces;

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
    public User Owner { get; set; }
}
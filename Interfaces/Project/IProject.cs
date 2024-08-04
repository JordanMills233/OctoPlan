using OctoPlan.Core.Enums;
using OctoPlan.Core.Models;

namespace OctoPlan.Core.Interfaces;

public interface IProject
{
    Guid Id { get; set; }
    string Title { get; set; }
    string Description { get; set; }
    DateTime StartDate { get; set; }
    DateTime EndDate { get; set; }
    Status Status { get; set; }
    Guid OwnerId { get; set; }
    User Owner { get; set; }
}
using OctoPlan.Core.Interfaces;

namespace OctoPlan.Core.Models;

public record User
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } 
    public string LastName { get; set; }
    public string Email { get; set; }
    public bool Registered { get; set; }
}
namespace OctoPlan.Core.Interfaces;

public interface IUser
{
    Guid Id { get; set; }
    string FirstName { get; set; }
    string LastName { get; set; }
    string Email { get; set; }
    bool Registered { get; set; }
}
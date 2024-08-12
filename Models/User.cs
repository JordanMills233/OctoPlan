using System.Security.Cryptography;
using System.Text;
using OctoPlan.Core.Models.Requests;


namespace OctoPlan.Core.Models;

public record User
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } 
    public string LastName { get; set; }
    public string Email { get; set; }
    public bool Registered { get; set; }
    public string PasswordHash { get; set; }

    public User()
    {
        
    }
    public User(CreateUserRequest request)
    {
        Id = Guid.NewGuid();
        FirstName = request.FirstName;
        LastName = request.LastName;
        Email = request.Email;
        PasswordHash = request.Password;
        Registered = false;
    }

    public User(string firstName, string lastName, string email)
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Registered = false;
    }
    
}


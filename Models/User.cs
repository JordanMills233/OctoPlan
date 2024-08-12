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
    public string Salt { get; set; }

    public User()
    {
        
    }
    public User(CreateUserRequest request, string salt)
    {
        Id = Guid.NewGuid();
        FirstName = request.FirstName;
        LastName = request.LastName;
        Email = request.Email;
        PasswordHash = request.Password;
        Salt = salt;
        Registered = false;
    }
}


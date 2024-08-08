using OctoPlan.Core.Interfaces;
using OctoPlan.Core.Models.Requests;
using FluentValidation;

namespace OctoPlan.Core.Models;

public record User
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } 
    public string LastName { get; set; }
    public string Email { get; set; }
    public bool Registered { get; set; }

    public User()
    {
        
    }
    public User(CreateUserRequest request)
    {
        Id = Guid.NewGuid();
        FirstName = request.FirstName;
        LastName = request.LastName;
        Email = request.Email;
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

    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(User => User.FirstName).MaximumLength(50);
        }
    }
}


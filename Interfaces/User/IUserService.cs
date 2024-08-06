using OctoPlan.Core.Models;

namespace OctoPlan.Core.Interfaces;

public interface IUserService
{
    Task<User> CreateUserAsync(User user);
    Task<User> GetUserByIdAsync(Guid userId);
}
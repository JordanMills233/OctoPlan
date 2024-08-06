using OctoPlan.Core.Models;

namespace OctoPlan.Core.Interfaces;

public interface IUserService
{
    Task<bool> CreateUserAsync(User user);
    Task<User> GetUserByIdAsync(Guid userId);
    Task<User> GetUserByEmailAsync(string email);
    Task UpdateUserAsync(User user, CancellationToken ct);
    Task DeleteUserAsync(Guid userId, CancellationToken ct);
}
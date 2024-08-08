using OctoPlan.Core.Models;
using OctoPlan.Core.Models.Requests;

namespace OctoPlan.Core.Interfaces;

public interface IUserService
{
    Task<bool> CreateUserAsync(CreateUserRequest request, CancellationToken ct);
    Task<User> GetUserByIdAsync(Guid userId);
    Task<User> GetUserByEmailAsync(string email);
    Task UpdateUserAsync(User user, CancellationToken ct);
    Task DeleteUserAsync(Guid userId, CancellationToken ct);
}
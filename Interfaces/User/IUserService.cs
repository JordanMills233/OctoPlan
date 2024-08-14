using OctoPlan.Core.Models;
using OctoPlan.Core.Models.Requests;
using OctoPlan.Core.Models.Responses;

namespace OctoPlan.Core.Interfaces;

public interface IUserService
{
    Task<bool> CreateUserAsync(CreateUserRequest request, CancellationToken ct);
    Task<User> GetUserByIdAsync(Guid userId);
    Task<User> GetUserByEmailAsync(string email);
    Task UpdateUserAsync(User user, CancellationToken ct);
    Task DeleteUserAsync(Guid userId, CancellationToken ct);
    Task<LoginResponse> LoginUserAsync(LoginRequest request, CancellationToken ct);
}
using Microsoft.EntityFrameworkCore;
using OctoPlan.Core.Interfaces;
using OctoPlan.Core.Models;
using OctoPlan.Core.Persistence;

namespace OctoPlan.Core.Services;

public class UserService : IUserService
{
    private readonly IDatabaseContext _databaseContext;

    public UserService(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }
    
    public async Task<bool> CreateUserAsync(User user)
    {
        try
        {
            await _databaseContext.Users.AddAsync(user);

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async Task<User> GetUserByIdAsync(Guid userId)
    {
        try
        {
            var user = await _databaseContext.Users.FirstOrDefaultAsync(u => u.Id.Equals(userId));

            if (user == null)
            {
                throw new Exception("No user exists");
            }

            return user;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        try
        {
            var user = await _databaseContext.Users.FirstOrDefaultAsync(u => u.Email.Equals(email));

            if (user == null)
            {
                throw new Exception("No user exists");
            }

            return user;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task UpdateUserAsync(User updatedUser, CancellationToken ct)
    {
        try
        {
            var user = await _databaseContext.Users.FirstOrDefaultAsync(u => u.Id.Equals(updatedUser.Id), ct);

            if (user == null)
            {
                throw new Exception("Couldnt find user");
            }

            user.FirstName = updatedUser.FirstName;
            user.LastName = updatedUser.LastName;
            user.Email = updatedUser.Email;
            user.Registered = updatedUser.Registered;

            await _databaseContext.SaveChangesAsync(ct);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }

    public async Task DeleteUserAsync(Guid userId, CancellationToken ct)
    {
        try
        {
            var user = await _databaseContext.Users.FirstOrDefaultAsync(u => u.Id.Equals(userId));
            
            if (user == null)
            {
                throw new Exception("No user found");
            }

            _databaseContext.Users.Remove(user);

            await _databaseContext.SaveChangesAsync(ct);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OctoPlan.Core.Interfaces;
using OctoPlan.Core.Models;
using OctoPlan.Core.Models.Requests;
using OctoPlan.Core.Models.Responses;
using OctoPlan.Core.Persistence;


namespace OctoPlan.Core.Services;

public class UserService : IUserService
{
    private readonly IDatabaseContext _databaseContext;
    private readonly IConfiguration _configuration;

    public UserService(IDatabaseContext databaseContext, IConfiguration configuration)
    {
        _databaseContext = databaseContext;
        _configuration = configuration;
    }
    
    public async Task<bool> CreateUserAsync(CreateUserRequest request, CancellationToken ct)
    {
        try
        {
            request.Password = HashPassword(request.Password, out var salt);
            var user = new User(request, Convert.ToHexString(salt));

            await _databaseContext.Users.AddAsync(user, ct);
            await _databaseContext.SaveChangesAsync(ct);

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
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

    public async Task<LoginResponse> LoginUserAsync(LoginRequest request, CancellationToken ct)
    {
        var user =
            await _databaseContext.Users.FirstOrDefaultAsync(u => u.Email.ToLower().Equals(request.Email.ToLower()), ct);

        if (user == null)
        {
            throw new Exception("User does not exist");
        }

        if (VerifyPassword(request.Password, user.PasswordHash))
        {
            return new LoginResponse(true, "Login Succesfull", GenerateJWTToken(user));
        };

        return new LoginResponse(false, "Login Failed");
    }

    private string GenerateJWTToken(User user)
    {
        var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? string.Empty));
        var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
        var userClaims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
            new Claim(ClaimTypes.Email, user.Email),
        };

        var token = new JwtSecurityToken(issuer: _configuration["Jwt:Issuer"], audience: _configuration["Jwt:Audience"],
            claims: userClaims, expires: DateTime.Now.AddHours(1), signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private string HashPassword(string password, out byte[] salt)
    {
        // Generate a salt
        salt = RandomNumberGenerator.GetBytes(16);

        // Hash the password using PBKDF2
        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256);
        byte[] hash = pbkdf2.GetBytes(32);

        // Combine the salt and hash
        byte[] hashBytes = new byte[48];
        Array.Copy(salt, 0, hashBytes, 0, 16);
        Array.Copy(hash, 0, hashBytes, 16, 32);

        // Convert to base64
        return Convert.ToBase64String(hashBytes);
    }
    
    private static bool VerifyPassword(string password, string storedHash)
    {
        // Extract the bytes from the stored hash (which includes the salt)
        byte[] hashBytes = Convert.FromBase64String(storedHash);

        // Extract the salt (first 16 bytes)
        byte[] salt = new byte[16];
        Array.Copy(hashBytes, 0, salt, 0, 16);

        // Hash the input password using the same salt and parameters
        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256);
        byte[] hash = pbkdf2.GetBytes(32);

        // Compare the computed hash with the stored hash (the last 32 bytes)
        for (int i = 0; i < 32; i++)
        {
            if (hashBytes[i + 16] != hash[i])
            {
                Console.WriteLine("password is wrong dog");
                return false;
            }
        }
        Console.WriteLine("password is right dog");
        return true;
    }
}